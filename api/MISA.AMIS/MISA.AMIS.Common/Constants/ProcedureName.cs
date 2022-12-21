using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Constants
{
    public class ProcedureName
    {
        /// <summary>
        /// Template cho procedure lấy tất cả bản ghi
        /// </summary>
        public static string PROC_GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Template cho procedure lấy bản ghi có lọc và phân trang
        /// </summary>
        public static string PROC_GET_PAGING = "Proc_{0}_GetPaging";
        /// <summary>
        /// Tempalte cho procedure lấy bản ghi theo iD
        /// </summary>
        public static string PROC_GET_BY_ID = "Proc_{0}_GetByID";

        /// <summary>
        /// Template cho procedure lấy bản ghi theo mã
        /// </summary>
        public static string PROC_GET_BY_CODE = "Proc_{0}_GetByCode";

        /// <summary>
        /// Template cho procedure lấy mã lớn nhất
        /// </summary>
        public static string PROC_GET_MAXCODE = "Proc_{0}_GetMaxCode";

        /// <summary>
        /// Template cho procedure thêm 1 bản ghi
        /// </summary>
        public static string PROC_INSERT_ONE = "Proc_{0}_Insert";

        /// <summary>
        /// Template cho procedure cập nhật 1 bản ghi
        /// </summary>
        public static string PROC_UPDATE_ONE = "Proc_{0}_Update";

        /// <summary>
        /// Template cho procedure xóa 1 bản ghi
        /// </summary>
        public static string PROC_DELETE_ONE = "Proc_{0}_DeleteOne";

        /// <summary>
        /// Template cho procedure xóa nhiều bản ghi
        /// </summary>
        public static string PROC_DELETE_MULTIPLE = "Proc_{0}_DeleteMultiple";
    }
}
