using Dapper;
using MISA.AMIS.Common.Constants;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {

        /// <summary>
        /// Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        public int GetMaxCode()
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_GET_MAXCODE, "Employee");

            // Chuẩn bị tham số đầu vào
            // Tạo connection string
            string connectionString = DatabaseContext.ConnectionString;

            int employeeMaxCode;

            // Khởi tạo kết nối database 
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Gọi vào DB để thực thi stored procedure
                employeeMaxCode = mySqlConnection.QueryFirstOrDefault<int>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

            }

            // Xử lí kết quả trả về
            return employeeMaxCode;
        }
    }
}
