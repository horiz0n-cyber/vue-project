using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Enums
{
    public enum AMISErrorCode
    {
        /// <summary>
        /// Gặp exception
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Dữ liệu đầu vào không hợp lệ
        /// </summary>
        InvalidData = 2,

        /// <summary>
        /// Trùng mã
        /// </summary>
        DuplicateCode = 3
    }
}
