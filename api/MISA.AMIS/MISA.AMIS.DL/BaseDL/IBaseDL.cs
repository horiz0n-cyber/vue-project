using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Hàm lấy bản ghi theo ID
        /// </summary>
        /// <param name="RecordID">ID bản ghi</param>
        /// <returns>Đối tượng bản ghi</returns>
        /// Created by: NTDUONG 29/11/2022
        public T GetRecordByID(Guid RecordID);

        /// <summary>
        /// Hàm lấy bản ghi theo mã 
        /// </summary>
        /// <param name="RecordCode">Mã bản ghi</param>
        /// <returns>Đối tượng bản ghi</returns>
        /// Created by: NTDUONG 03/11/2022
        public T GetRecordByCode(string RecordCode);

        /// <summary>
        /// Lấy danh sách bản ghi có phân trang
        /// </summary>
        /// <param name="where">chuỗi điều kiện lọc</param>
        /// <param name="limit">tham số limnit của stored procedure</param>
        /// <param name="offset">tham số offset của stored procedure</param>
        /// <returns></returns>
        public PagingResult<T> GetRecordsPaging(string? where, int limit, int? offset);

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllRecords();
        
        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">đối tượng bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 30/11/2022
        public int InsertRecord(T record);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="record">đối tượng bản ghi</param>
        /// <param name="RecordID">ID bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 29/11/2022
        public int UpdateRecord(T record, Guid RecordID);

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="RecordID">đối tượng bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 30/11/2022
        public int DeleteOneRecord(Guid RecordID);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="RecordIDCommaArray">Chỗi id của các bản ghi cần xóa, phân tách bởi dấu phẩy</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 05/12/2022
        public int DeleteMultipe(string recordIDCommaString);
    }
}
