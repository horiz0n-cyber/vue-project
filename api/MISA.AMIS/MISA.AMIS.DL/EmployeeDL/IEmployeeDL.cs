using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        public int GetMaxCode();
    }
}
