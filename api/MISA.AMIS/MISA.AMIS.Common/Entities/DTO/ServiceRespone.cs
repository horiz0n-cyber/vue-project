using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class ServiceRespone
    {

        /// <summary>
        /// Trạng thái thực thi các tác vụ
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
    }
}
