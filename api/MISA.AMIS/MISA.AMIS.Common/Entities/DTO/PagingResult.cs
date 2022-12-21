using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    /// <summary>
    /// Dữ liệu trả về khi phân trang
    /// </summary>
    public class PagingResult<T>
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public long TotalRecord { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<T> Data { get; set; }    
    }
}
