using MISA.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public AMISErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Message lỗi dành cho developer
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Message lỗi dành cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin thêm về lỗi
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// Id để trace lỗi
        /// </summary>
        public string TraceId  { get; set; }
    }
}
