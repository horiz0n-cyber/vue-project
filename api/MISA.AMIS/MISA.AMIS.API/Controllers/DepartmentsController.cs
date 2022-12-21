using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.API.Controllers
{
    public class DepartmentsController : BaseController<Department>
    {
        #region Field

        private IDepartmentBL _departmentBL;

        #endregion

        #region Constructor

        public DepartmentsController(IDepartmentBL departmentBL) : base(departmentBL)
        {
            _departmentBL = departmentBL;
        }

        #endregion
    }
}
