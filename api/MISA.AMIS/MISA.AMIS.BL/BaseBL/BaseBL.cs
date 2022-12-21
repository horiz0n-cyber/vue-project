using MISA.AMIS.Common.Attributes;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {

        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseBL)
        {
            _baseDL = baseBL;
        }

        #endregion

        #region Method
        /// <summary>
        /// Hàm lấy bản ghi theo ID
        /// </summary>
        /// <param name="RecordID">ID bản ghi</param>
        /// <returns>Đối tượng bản ghi</returns>
        /// Created by: NTDUONG 29/11/2022
        public T GetRecordByID(Guid RecordID)
        {
            // Gọi đến DL, trả về bản ghi theo id
            return _baseDL.GetRecordByID(RecordID);
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: NTDUONG 2/12/2022
        public IEnumerable<T> GetAllRecords()
        {
            // Gọi đến DL, trả về danh sách tất cả bản ghi
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy danh sách bản ghi có phân trang
        /// </summary>
        /// <param name="filterString">Điều kiện lọc</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang hiện tại</param>
        /// <returns>đối tượng gồm danh sách bản ghi và tổng số bản ghi</returns>
        /// Created by: NTDUONG 02/12/2022
        public PagingResult<T> GetRecordsPaging(string? filterString, int pageSize, int? pageNumber)
        {

            string where = null;
            // Ghép câu lệnh tìm kiếm
            if(!String.IsNullOrEmpty(filterString))
            {
                where = $"{typeof(T).Name}Code LIKE '%{filterString}%' OR {typeof(T).Name}Name LIKE '%{filterString}%'";
            }

            // Tính offset của stored procedure
            int? offset = (pageNumber - 1) * pageSize;

            if(pageSize == null)
            {
                pageSize = -1;
            }

            // Gọi đến DL, trả về đối tượng PagingResult 
            return _baseDL.GetRecordsPaging(where, pageSize, offset);
        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">đối tượng bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 30/11/2022
        public ServiceRespone InsertRecord(T record)
        {
            // Tạo mảng chứa lỗi
            List<string> validateErrors = new List<string>();

            // gọi đến hàm vaidate các trường bắt buộc
            ValidateRequired(validateErrors, record);
            if(validateErrors.Count > 0)
            {
                return new ServiceRespone
                {
                    IsError = true,
                    Data = validateErrors
                };
            }

            // Gọi đến hàm validate mã bản ghi kết thức bằng số hay không
            ValidateCode(validateErrors, record);
            if (validateErrors.Count > 0)
            {
                return new ServiceRespone
                {
                    IsError = true,
                    Data = validateErrors
                };
            }

            // Gọi đến hàm validate kiểm tra mã trùng
            ValidateUnique(validateErrors, record);
            if (validateErrors.Count > 0)
            {
                return new ServiceRespone
                {
                    IsError = true,
                    Data = validateErrors
                };
            }

            // Gọi đến DL để thực thi thêm bản ghi vào DB
            int numberOfAffectionRows = _baseDL.InsertRecord(record);

            return new ServiceRespone
            {
                IsError = false,
                Data = numberOfAffectionRows
            };
        }

        /// <summary>
        /// Validate các trường bắt buộc
        /// </summary>
        /// <param name="validateErrors">danh sách tring chứa lỗi</param>
        /// <param name="record">bản ghi</param>
        /// Created by: NTDUONG 03/12/2022
        public void ValidateRequired(List<string> validateErrors, T record)
        {
            // Lấy property của đối tượng
            var properties = typeof(T).GetProperties();

            // Duyệt attribute của property 
            foreach(var property in properties)
            {
                // lấy giá trị của prop
                var propertyValue = property.GetValue(record);

                var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();

                // nếu prop có attribute là Required, tiến hành check null
                if(requiredAttribute != null)
                {
                    if(propertyValue == null)
                    {
                        validateErrors.Add(requiredAttribute.ErrorMessage);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(propertyValue.ToString().Trim()))
                        {
                            validateErrors.Add(requiredAttribute.ErrorMessage);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Validate mã bản ghi đúng định dạng 
        /// </summary>
        /// <param name="validateErrors">danh sách tring chứa lỗi</param>
        /// <param name="record">bản ghi</param>
        /// Created by: NTDUONG 03/12/2022
        public void ValidateCode(List<string> validateErrors, T record)
        {
            // Lấy property của đối tượng
            var properties = typeof(T).GetProperties();

            // Duyệt attribute của property 
            foreach (var property in properties)
            {
                // lấy giá trị của prop
                var propertyValue = property.GetValue(record);
               
                var validCodeAttribute = (ValidCode)property.GetCustomAttributes(typeof(ValidCode), false).FirstOrDefault();

                // nếu prop có attribute là ValidCode, tiến hành kiểm tra chuỗi mã
                if (validCodeAttribute != null)
                {
                    string recordCode = propertyValue.ToString().Trim();
                    char lastOfRecordCode = recordCode[recordCode.Length - 1];

                    // Nếu kí tự cuối khác từ 0-9 -> gán lỗi vào list validateErrors
                    if (lastOfRecordCode < char.Parse("0") || lastOfRecordCode > char.Parse("9"))
                    {
                        validateErrors.Add(validCodeAttribute.ErrorMessage);
                    }
                }

            }

        }

        /// <summary>
        /// Validate mã bản ghi bị trùng
        /// </summary>
        /// <param name="validateErrors">danh sách tring chứa lỗi</param>
        /// <param name="record">bản ghi</param>
        /// Created by: NTDUONG 03/12/2022
        public void ValidateUnique(List<string> validateErrors, T record)
        {
            // Lấy property của đối tượng
            var properties = typeof(T).GetProperties();

            // Duyệt attribute của property 
            foreach (var property in properties)
            {
                // lấy giá trị của prop
                var propertyValue = property.GetValue(record);

                var uniqueAttribute = (Unique)property.GetCustomAttributes(typeof(Unique), false).FirstOrDefault();

                // nếu prop có attribute là Unique, tiến hành kiểm tra chuỗi mã
                if (uniqueAttribute != null)
                {
                    // Gọi DL lấy bản ghi theo code
                    T recordByCode= _baseDL.GetRecordByCode(propertyValue.ToString());
                    
                    // Nếu bản ghi tồn tại -> gán lỗi vào mảng validateErrors
                    if (recordByCode != null)
                    {
                        validateErrors.Add(String.Format(uniqueAttribute.ErrorMessage, propertyValue.ToString()));
                    }
                }

            }

        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="record">đối tượng bản ghi</param>
        /// <param name="RecordID">ID bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 29/11/2022
        public ServiceRespone UpdateRecord(T record, Guid RecordID)
        {
            // Tạo mảng chứa lỗi
            List<string> validateErrors = new List<string>();

            // gọi đến hàm vaidate các trường bắt buộc
            ValidateRequired(validateErrors, record);
            if (validateErrors.Count > 0)
            {
                return new ServiceRespone
                {
                    IsError = true,
                    Data = validateErrors
                };
            }

            // Gọi đến hàm validate mã bản ghi kết thức bằng số hay không
            ValidateCode(validateErrors, record);
            if (validateErrors.Count > 0)
            {
                return new ServiceRespone
                {
                    IsError = true,
                    Data = validateErrors
                };
            }

            // Lấy dữ liệu bản ghi theo ID
            T recordBeforeUpdate = _baseDL.GetRecordByID(RecordID);
            string recordCodeBeforeUpdate = typeof(T).GetProperty($"{typeof(T).Name}Code").GetValue(recordBeforeUpdate).ToString();
            string recordCodePrepareUpdate = typeof(T).GetProperty($"{typeof(T).Name}Code").GetValue(record).ToString();
            if(!String.Equals(recordCodeBeforeUpdate, recordCodePrepareUpdate)){
                // Gọi đến hàm validate kiểm tra mã trùng
                ValidateUnique(validateErrors, record);
                if (validateErrors.Count > 0)
                {
                    return new ServiceRespone
                    {
                        IsError = true,
                        Data = validateErrors
                    };
                }

            }

            // Gọi đến DL để thực thi thêm bản ghi vào DB
            int numberOfAffectionRows = _baseDL.UpdateRecord(record, RecordID);

            return new ServiceRespone
            {
                IsError = false,
                Data = numberOfAffectionRows
            };
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="RecordID">đối tượng bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 30/11/2022
        public int DeleteOneRecord(Guid RecordID)
        {
            // trả về số bản ghi bị ảnh hưởng
            return _baseDL.DeleteOneRecord(RecordID);
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="RecordIDCommaArray">Chuỗi id của các bản ghi cần xóa, phân tách bởi dấu phẩy</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 05/12/2022
        public int DeleteMultipe(Guid[] recordIDArray)
        {
            // join các phần tử trong mảng thành chuỗi phân tách bằng dấu phẩy
            string recordIDCommaString = string.Join(",", recordIDArray);

            // Gọi đến DL, trả về số bản ghi bị ảnh hưởng
            return _baseDL.DeleteMultipe(recordIDCommaString);
        }

        

        #endregion
    }
}
