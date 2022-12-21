// Enums dùng chung cho toàn chương trình
var Enum = Enum || {}

// Giới tính
Enum.Gender = {
    Female: 0,  // Nữ
    Male: 1,    // Nam
    Other: 2    // Khác
}

// form mode
Enum.FormMode = {
    Add: 1,         // Thêm
    Edit: 2,        // Sửa
    Duplicate: 3    // Nhân bản
}

// action form
Enum.FormAction = {
    SaveAndClose: 1,
    SaveAndAdd: 2,
    Close: 3
}

// dialog mode
Enum.DialogMode = {
    Question: 1,
    Error: 2,
    Warning: 3,
    WarningMulti: 4
}

export default Enum