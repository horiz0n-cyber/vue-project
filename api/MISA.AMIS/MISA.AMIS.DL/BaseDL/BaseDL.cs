using Dapper;
using MISA.AMIS.Common.Constants;
using MISA.AMIS.Common.Entities;
using MySqlConnector;

namespace MISA.AMIS.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {

        /// <summary>
        /// Hàm lấy bản ghi theo ID
        /// </summary>
        /// <param name="RecordID">ID bản ghi</param>
        /// <returns>Đối tượng bản ghi</returns>
        /// Created by: NTDUONG 29/11/2022
        public T GetRecordByID(Guid RecordID)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_GET_BY_ID, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", RecordID);

            // Chuẩn bị connection string
            string connectionString = DatabaseContext.ConnectionString;

            // Khởi tạo kết nối DB
            T record;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Gọi vào DB để thực thi stored procedure
                record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
            // Xử lí kết quả trả về
            return record;
        }

        /// <summary>
        /// Hàm lấy bản ghi theo mã 
        /// </summary>
        /// <param name="RecordCode">Mã bản ghi</param>
        /// <returns>Đối tượng bản ghi</returns>
        /// Created by: NTDUONG 03/11/2022
        public T GetRecordByCode(string RecordCode)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_GET_BY_CODE, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}Code", RecordCode);

            // Chuẩn bị connection string
            string connectionString = DatabaseContext.ConnectionString;

            // Khởi tạo kết nối DB
            T record;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Gọi vào DB để thực thi stored procedure
                record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            // Xử lí kết quả trả về
            return record;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: NTDUONG 2/12/2022
        public IEnumerable<T> GetAllRecords()
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_GET_ALL, typeof(T).Name);

            // Chuẩn bị connection string
            string connectionString = DatabaseContext.ConnectionString;

            IEnumerable<T> records;
            
            // Khởi tạo kết nối đến DB
            using(var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Gọi vào DB để thực thi stored procedure
                records = mySqlConnection.Query<T>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
            }

            // Trả về kết quả cho BL
            return records;
        }

        /// <summary>
        /// Lấy danh sách bản ghi có phân trang và tìm kiếm
        /// </summary>
        /// <param name="where">chuỗi điều kiện lọc</param>
        /// <param name="limit">tham số limnit của stored procedure</param>
        /// <param name="offset">tham số offset của stored procedure</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by NTDUONG 02/12/2022
        public PagingResult<T> GetRecordsPaging(string? where, int limit, int? offset)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_GET_PAGING, typeof(T).Name);

            // Chuẩn bị tham số tuyền vào
            var parameters = new DynamicParameters();
            parameters.Add("@Where", where);
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", offset);
            parameters.Add("@Sort", null);

            PagingResult<T> pagingResult = new PagingResult<T>();

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var queryResult = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType:System.Data.CommandType.StoredProcedure);
                pagingResult.Data = queryResult.Read<T>().ToList();
                pagingResult.TotalRecord = queryResult.ReadSingle<long>();
            }

            return pagingResult;
        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">đối tượng bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 30/11/2022
        public int InsertRecord(T record)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_INSERT_ONE, typeof(T).Name);

            // Lấy toàn bộ properties của bản ghi
            var properties = typeof(T).GetProperties();

            // Chuẩn bị tham số đầu vào cho stored procedure 
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                // Nếu property là RecordID -> gán giá trị new guid vào param
                if (string.Equals(property.Name, $"{typeof(T).Name}ID"))
                {
                    parameters.Add($"@{property.Name}", Guid.NewGuid());
                }
                // Nếu property là CreatedDate hoặc ModifiedDate -> gán giá trị ngày hiện tại vào param
                else if (string.Equals(property.Name, "CreatedDate") || string.Equals(property.Name, "ModifiedDate"))
                {
                    parameters.Add($"@{property.Name}", DateTime.Now);
                }
                // Các trường hợp còn lại lấy giá trị của prop và gán vào param
                else
                {
                    parameters.Add($"@{property.Name}", property.GetValue(record));
                }
            }

            // Khởi tạo kết nối DB
            string connectionString = DatabaseContext.ConnectionString;

            int numberOfAffectioRows;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Gọi vào DB để thực thi stored procedure
                numberOfAffectioRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            // Xử lí kết quả trả về
            return numberOfAffectioRows;
        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="record">đối tượng bản ghi</param>
        /// <param name="RecordID">ID bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 29/11/2022
        public int UpdateRecord(T record, Guid RecordID)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_UPDATE_ONE, typeof(T).Name);

            // Lấy toàn bộ properties của bản ghi
            var properties = typeof(T).GetProperties();

            // Chuẩn bị tham số đầu vào cho stored procedure 
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                // Nếu property là CreatedDate hoặc ModifiedDate -> gán giá trị ngày hiện tại vào param
                if (string.Equals(property.Name, "ModifiedDate"))
                {
                    parameters.Add($"@{property.Name}", DateTime.Now);
                }
                // Nếu property là CreatedDate hoặc CreatedBy -> bỏ qua
                else if (string.Equals(property.Name, "CreatedDate") || string.Equals(property.Name, "CreatedBy"))
                {
                    continue;
                }
                // Nếu property là RecordID -> gán giá trị new guid vào param
                else if (string.Equals(property.Name, $"{typeof(T).Name}ID"))
                {
                    parameters.Add($"@{property.Name}", RecordID);
                }
                // Các trường hợp còn lại lấy giá trị của prop và gán vào param
                else
                {
                    parameters.Add($"@{property.Name}", property.GetValue(record));
                }
            }

            // Khởi tạo kết nối DB
            string connectionString = DatabaseContext.ConnectionString;

            int numberOfAffectioRows;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Gọi vào DB để thực thi stored procedure
                numberOfAffectioRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            // Xử lí kết quả trả về
            return numberOfAffectioRows;
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="RecordID">đối tượng bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 30/11/2022
        public int DeleteOneRecord(Guid RecordID)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_DELETE_ONE, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", RecordID);

            // Khởi tạo kết nối DB
            string connectionString = DatabaseContext.ConnectionString;

            int numberOfAffectioRows;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Gọi vào DB để thực thi stored procedure
                numberOfAffectioRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            // Xử lí kết quả trả về
            return numberOfAffectioRows;
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="RecordIDCommaArray">Chỗi id của các bản ghi cần xóa, phân tách bởi dấu phẩy</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 05/12/2022
        public int DeleteMultipe(string recordIDCommaString)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_DELETE_MULTIPLE, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}IDArray", recordIDCommaString);
            
            // CHuẩn bị connectionString
            string connectionString = DatabaseContext.ConnectionString;

            // Khởi tạo kết nối DB
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                mySqlConnection.Open();
                // Khởi tạo transaction
                var transaction = mySqlConnection.BeginTransaction();
                try
                {
                    // Gọi vào DB để thực thi stored procedure
                    int numberOfAffectioRows = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    // Nếu thành công, commit  và return số bản ghi ảnh hưởng
                    transaction.Commit();
                    return numberOfAffectioRows;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                    return 0;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }
    }
}
