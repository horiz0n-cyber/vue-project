// Resources dùng chung cho toàn chương trình
var Resource = Resource || {}

// api domain
Resource.Domain = "http://localhost:5233/";

// Các api dùng trong chương trình
Resource.APIs = {
    Employees: Resource.Domain + "api/v1/Employees",
    Departments: Resource.Domain + "api/v1/Departments",
}

// Giới tính
Resource.Gender = {
    Female: "Nữ",
    Male: "Nam",
    Other: "Khác"
}

// Tiều đề form
Resource.FormTitle = {
    Add: "Thêm thông tin nhân viên",
    Edit: "Sửa thông tin nhân viên"
}

Resource.InputError = {
    Required: " không được để trống",
    InvalidDate: " không được lớn hơn ngày hiện tại"
}

// Thông báo lỗi form
Resource.FieldError = {
    EmployeeNameRequired: "Tên" + Resource.InputError.Required,
    EmployeeCodeRequired: "Mã" + Resource.InputError.Required,
    DepartmentIdRequired: "Đơn vị " + Resource.InputError.Required,
    DateOfBirthInvalid: "Ngày sinh " + Resource.InputError.InvalidDate,
    IdentityIssuedDateInvalid: "Ngày cấp " + Resource.InputError.InvalidDate
} 

// Thông báo của toast message
Resource.ToastMess = {
    AddEmployeeSuccess: "Thêm nhân viên mới thành công",
    EditEmployeeSuccess: "Sửa nhân viên thành công",
    DeleteEmployeeSuccess: "Nhân viên đã bị xóa",
}

export default Resource 