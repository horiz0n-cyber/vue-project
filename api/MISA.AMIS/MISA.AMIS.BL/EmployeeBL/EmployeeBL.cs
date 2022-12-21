using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Resource;
using MISA.AMIS.DL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        public string GetNewCode()
        {
            int maxCode =  _employeeDL.GetMaxCode();

            string newCode = "NV-" + (maxCode + 1);

            return newCode;
        }

        public byte[] ExportToExcel(string? filterString)
        {
            // Lấy danh sách bản ghi cần export
            PagingResult<Employee> pagingResult = GetRecordsPaging(filterString, -1, null);

            List<Employee> records = pagingResult.Data;

            using ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            Color colorFromHex = System.Drawing.ColorTranslator.FromHtml("#BDBDBD");

            // Lấy property của đối tượng
            var properties = typeof(EmployeeExcelDTO).GetProperties();

            // Duyệt attribute của property, binding dữ liệu vào header bảng
            int colIndex = 2;
            foreach (var property in properties)
            {
                workSheet.Cells[3, colIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, colIndex].Style.Fill.BackgroundColor.SetColor(colorFromHex);
                workSheet.Cells[3, colIndex].Value = Resources.ResourceManager.GetString(property.Name);
                colIndex++;
            }

            //Binding dữ liệu vào thân bảng
            int recordIndex = 4;
            foreach (var record in records)
            {
                workSheet.Cells[recordIndex, 1].Value = (recordIndex - 3).ToString();

                int collumIndex = 2;
                // Duyệt attribute của property 
                foreach (var property in properties)
                {
                    // lấy giá trị của prop
                    var propertyValue = record.GetType().GetProperty(property.Name).GetValue(record, null);

                    // Gán vào ô trong bảng
                    if (propertyValue == null)
                    {
                        workSheet.Cells[recordIndex, collumIndex].Value = "";
                    }
                    else
                    {
                        // Nếu propperty là EmployeeName => viết hoa
                        if (property.Name == "EmployeeName")
                        {
                            workSheet.Cells[recordIndex, collumIndex].Value = propertyValue.ToString().ToUpper();
                        }
                        else
                        {
                            handleNonStringCell(workSheet, recordIndex, collumIndex, propertyValue);
                        }
                    }
                    collumIndex++;
                }
                recordIndex++;
            }

            // style cho bảng
            StylingTable(workSheet, colIndex, recordIndex, colorFromHex);

            for (int i = 1; i <= colIndex; i++)
            {
                workSheet.Column(i).AutoFit();
            }
            return excel.GetAsByteArray();
        }

        /// <summary>
        /// Hàm style cho bảng excel
        /// </summary>
        /// <param name="workSheet">work sheet của excel</param>
        /// <param name="colIndex">tổng số cột của bảng</param>
        /// <param name="recordIndex">tổng số hàng dữ liệu</param>
        /// <param name="colorFromHex">màu header bảng</param>
        private static void StylingTable(ExcelWorksheet workSheet, int colIndex, int recordIndex, Color colorFromHex)
        {
            //set tên bảng 
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = Resources.EmployeeExcelName.ToUpper();
            workSheet.Cells[1, 1].Style.Font.Name = "Arial";
            workSheet.Cells[1, 1].Style.Font.Size = 16;

            // merge cột ở dòng 1 và 2 
            workSheet.Cells[1, 1, 1, colIndex - 1].Merge = true;
            workSheet.Cells[2, 1, 2, colIndex - 1].Merge = true;

            //Tạo và style phần header cho bảng
            workSheet.Row(3).Height = 20;
            workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Row(3).Style.Font.Bold = true;
            workSheet.Row(3).Style.Font.Name = "Arial";
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[3, 1].Style.Fill.BackgroundColor.SetColor(colorFromHex);

            // tạo border cell
            workSheet.Cells[3, 1, recordIndex - 1, colIndex - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[3, 1, recordIndex - 1, colIndex - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[3, 1, recordIndex - 1, colIndex - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[3, 1, recordIndex - 1, colIndex - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            // thay đổi font style
            workSheet.Cells[4, 1, recordIndex - 1, colIndex - 1].Style.Font.Name = "Times New Roman";
        }

        /// <summary>
        /// Hàm xử lí giá trị để bind vào cell
        /// </summary>
        /// <param name="workSheet">work sheet của excel</param>
        /// <param name="recordIndex">vị trí hàng của cell</param>
        /// <param name="collumIndex">vị trí cột của cell</param>
        /// <param name="propertyValue">giá trị của property</param>
        private static void handleNonStringCell(ExcelWorksheet workSheet, int recordIndex, int collumIndex, object? propertyValue)
        {
            switch (propertyValue.GetType().Name)
            {
                case "Gender":
                    workSheet.Cells[recordIndex, collumIndex].Value = Resources.ResourceManager.GetString(propertyValue.ToString());
                    break;
                case "DateTime":
                    var date = DateTime.Parse(propertyValue.ToString());
                    workSheet.Cells[recordIndex, collumIndex].Value = date.ToString("dd/MM/yyyy");
                    workSheet.Cells[recordIndex, collumIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    break;
                default:
                    workSheet.Cells[recordIndex, collumIndex].Value = propertyValue.ToString();
                    break;
            }
        }

        #endregion
    }
}
