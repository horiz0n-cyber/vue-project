using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class Base
    {
        /// <summary>
        /// Ngày thêm 
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người thêm 
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
