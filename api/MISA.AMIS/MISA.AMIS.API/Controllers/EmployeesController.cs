using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.Common.Resource;

namespace MISA.AMIS.API.Controllers
{
    public class EmployeesController : BaseController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Contructor

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method

        [HttpGet]
        [Route("new-code")]
        public IActionResult GetNewCode()
        {
            try
            {
                var newCode = _employeeBL.GetNewCode();
                return StatusCode(StatusCodes.Status200OK, newCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = AMISErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.ErrorURL,
                    TraceId = HttpContext.TraceIdentifier
                });
            }

        }

        [HttpGet]
        [Route("export")]
        public IActionResult ExportRecordsToExcel([FromQuery] string? filterString)
        {
            try
            {
                var exportbytes = _employeeBL.ExportToExcel(filterString);
                string excelName = $"{Resources.EmployeeExcelName}.xlsx";
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = AMISErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.ErrorURL,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        #endregion
    }
}
