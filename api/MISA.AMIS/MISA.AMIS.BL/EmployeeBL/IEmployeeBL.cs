using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        #region Method

        /// <summary>
        /// Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        public string GetNewCode();


        public byte[] ExportToExcel(string? filterString);

        #endregion
    }
}
