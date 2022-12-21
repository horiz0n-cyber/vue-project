using MISA.AMIS.Common.Attributes;
using MISA.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities 
{
    /// <summary>
    /// Đối tượng nhân viên
    /// </summary>
    public class Employee : Base
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        public Guid? EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        [ValidCode("Mã nhân viên phải kết thúc bằng số")]
        [Unique("Mã nhân viên {0} đã tồn tại trong hệ thống")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        public string? EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh 
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Số căn cước / chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityIssuedDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IdentityIssuedPlace { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [Required(ErrorMessage = "Đơn vị không được để trống")]
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Email nhân viên
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Địa chí
        /// </summary>
        public string? Address { get; set; }
    }
}
