using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Hàm lấy bản ghi theo ID
        /// </summary>
        /// <param name="RecordID">ID bản ghi</param>
        /// <returns></returns>
        /// Created by: NTDUONG 29/11/2022
        public T GetRecordByID(Guid RecordID);

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: NTDUONG 02/12/2022
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Lấy danh sách bản ghi có phân trang
        /// </summary>
        /// <param name="filterString">Điều kiện lọc</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang hiện tại</param>
        /// <returns>đối tượng gồm danh sách bản ghi và tổng số bản ghi</returns>
        /// Created by: NTDUONG 02/12/2022
        public PagingResult<T> GetRecordsPaging(string? filterString, int pageSize, int? pageNumber);

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">đối tượng bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 30/11/2022
        public ServiceRespone InsertRecord(T record);

        /// <summary>
        /// Validate các trường bắt buộc
        /// </summary>
        /// <param name="validateErrors">danh sách tring chứa lỗi</param>
        /// <param name="record">bản ghi</param>
        public void ValidateRequired(List<string> validateErrors, T record);

        /// <summary>
        /// Validate mã bản ghi hợp lệ
        /// </summary>
        /// <param name="validateErrors">danh sách tring chứa lỗi</param>
        /// <param name="record">bản ghi</param>
        public void ValidateCode(List<string> validateErrors, T record);

        /// <summary>
        /// Validate trùng mã bản ghi
        /// </summary>
        /// <param name="validateErrors">danh sách tring chứa lỗi</param>
        /// <param name="record">bản ghi</param>
        public void ValidateUnique(List<string> validateErrors, T record);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="record">đối tượng bản ghi</param>
        /// <param name="RecordID">ID bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 29/11/2022
        public ServiceRespone UpdateRecord(T record, Guid RecordID);

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
        public int DeleteMultipe(Guid[] recordIDArray);

    }
}
