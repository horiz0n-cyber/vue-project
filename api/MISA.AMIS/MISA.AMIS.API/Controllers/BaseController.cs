using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.Common.Resource;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API lấy bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                // Gọi đển BL để lấy bản ghi theo id
                var record = _baseBL.GetRecordByID(recordID);

                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resources.DevMsg_InvalidData,
                        UserMsg = Resources.UserMsg_InvalidData,
                        MoreInfo = "https://example.com/errorcode/1",
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = AMISErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = "https://example.com/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>danh sách bản ghi</returns>
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                // Gọi đến BL lấy tất cả bản ghi
                IEnumerable<T> records = _baseBL.GetAllRecords();

                // nếu dữ liệu hợp lệ, trả về cho client
                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resources.DevMsg_InvalidData,
                        UserMsg = Resources.UserMsg_InvalidData,
                        MoreInfo = Resources.ErrorURL,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// API lấy danh sách bản ghi có phân trang
        /// </summary>
        /// <param name="filterString">chuỗi tìm kiếm</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">số trang</param>
        /// <returns></returns>
        [HttpGet]
        [Route("filter")]
        public IActionResult GetRecordsPaging([FromQuery] string? filterString, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            try
            {
                // Gọi đến BL để lấy dữ liệu
                PagingResult<T> pagingResult = _baseBL.GetRecordsPaging(filterString, pageSize, pageNumber);

                // Nếu dữ liệu hợp lệ, trả về cho client
                if (pagingResult != null)
                {
                    return StatusCode(StatusCodes.Status200OK, pagingResult);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resources.DevMsg_InvalidData,
                        UserMsg = Resources.UserMsg_InvalidData,
                        MoreInfo = Resources.ErrorURL,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// API thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                var serviceRespone = _baseBL.InsertRecord(record);

                if (!serviceRespone.IsError)
                {
                    return StatusCode(StatusCodes.Status201Created, serviceRespone.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resources.DevMsg_InvalidData,
                        UserMsg = Resources.UserMsg_InvalidData,
                        MoreInfo = serviceRespone.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// API sửa 1 bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{recordID}")]
        public IActionResult UpdateRecord(T record, Guid recordID)
        {
            try
            {
                var serviceRespone = _baseBL.UpdateRecord(record, recordID);

                if (!serviceRespone.IsError)
                {
                    return StatusCode(StatusCodes.Status200OK, serviceRespone.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resources.DevMsg_InvalidData,
                        UserMsg = Resources.UserMsg_InvalidData,
                        MoreInfo = serviceRespone.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// API xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{recordID}")]
        public IActionResult DeleteOneRecord([FromRoute] Guid recordID)
        {
            try
            {
                // Gọi đến BL để xóa bản ghi theo ID
                var numberOfAffectionRows = _baseBL.DeleteOneRecord(recordID);

                if (numberOfAffectionRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, recordID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resources.DevMsg_InvalidData,
                        UserMsg = Resources.UserMsg_InvalidData,
                        MoreInfo = Resources.ErrorURL,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
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

        /// <summary>
        /// API Xóa nhiều bản ghi
        /// </summary>
        /// <param name="RecordIDCommaArray">Chuỗi id của các bản ghi cần xóa, phân tách bởi dấu phẩy</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NTDUONG 05/12/2022
        [HttpDelete]
        public IActionResult DeleteMultipe([FromBody] Guid[] recordIDArray)
        {
            try
            {
                // Gọi đến BL để xóa bản ghi theo ID
                var numberOfAffectionRows = _baseBL.DeleteMultipe(recordIDArray);

                if (numberOfAffectionRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, numberOfAffectionRows);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resources.DevMsg_InvalidData,
                        UserMsg = Resources.UserMsg_InvalidData,
                        MoreInfo = Resources.ErrorURL,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
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
