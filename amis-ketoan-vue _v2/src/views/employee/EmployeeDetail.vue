<template>
  <!--form chi tiết nhân viên  -->
  <div id="EmployeeForm" class="form-container">
    <div class="form">
      <div class="form-main">
        <div class="form-header">
          <div class="form-title">{{ returnFormTitle }}</div>
          <label
            class="checkbox-container form-checkbox flex align-item-center"
          >
            <input type="checkbox" name="info" />
            <span class="checkmark"></span>
            <span class="checkbox-label">Là khách hàng</span>
          </label>
          <label
            class="checkbox-container form-checkbox flex align-item-center"
          >
            <input type="checkbox" name="info" />
            <span class="checkmark"></span>
            <span class="checkbox-label">Là nhà cung cấp</span>
          </label>
        </div>
        <div class="form-body">
          <!-- input chia 2 cột -->
          <div class="flex">
            <!-- collumn input -->
            <div class="w-1/2 p-r-26">
              <div class="flex row-input">
                <div class="w-2/5 p-r-6">
                  <div class="input-wrapper">
                    <div class="label-input">
                      Mã <span class="cl-red">*</span>
                    </div>
                    <div class="input-container">
                      <input
                        tabindex="1"
                        ref="empCodeInput"
                        class="ms-input"
                        type="text"
                        :title="errors.employeeCode"
                        v-model="employee.employeeCode"
                        :class="{ 'ms-input-error': errors.employeeCode }"
                        @blur="checkBlurRequied('employeeCode')"
                        @focus="resetError('employeeCode')"
                      />
                    </div>
                    <div class="error-mess" v-if="errors.employeeCode">
                      {{ errors.employeeCode }}
                    </div>
                  </div>
                </div>
                <div class="w-3/5">
                  <div class="input-wrapper">
                    <div class="label-input">
                      Tên <span class="cl-red">*</span>
                    </div>
                    <div class="input-container">
                      <input
                        tabindex="2"
                        class="ms-input"
                        type="text"
                        :title="errors.employeeName"
                        v-model="employee.employeeName"
                        :class="{ 'ms-input-error': errors.employeeName }"
                        @blur="checkBlurRequied('employeeName')"
                        @focus="resetError('employeeName')"
                      />
                    </div>
                    <div class="error-mess" v-if="errors.employeeName">
                      {{ errors.employeeName }}
                    </div>
                  </div>
                </div>
              </div>
              <div class="input-wrapper">
                <div class="label-input">
                  Đơn vị <span class="cl-red">*</span>
                </div>
                <MISADropdown
                  ref="departmentDropdown"
                  @checkRequied="checkBlurRequied('departmentID')"
                  @resetError="resetError('departmentID')"
                  @parseValue="getDropdownData"
                  :title="errors.departmentID"
                  :data="departments"
                  :itemID="departmentID"
                  :itemName="departmentName"
                  :state="errors.departmentID"
                ></MISADropdown>
                <div class="error-mess" v-if="errors.departmentID">
                  {{ errors.departmentID }}
                </div>
              </div>
              <div class="input-wrapper">
                <div class="label-input">Chức danh</div>
                <div class="input-container">
                  <input
                    v-model="employee.positionName"
                    tabindex="8"
                    class="ms-input"
                    type="text"
                  />
                </div>
              </div>
            </div>
            <!-- collum input -->
            <div class="w-1/2">
              <div class="flex row-input">
                <div class="input-wrapper">
                  <div for="" class="label-input">Ngày sinh</div>
                  <MISAInputDate 
                  tabindex="3"
                  property="dateOfBirth" 
                  :value="employee.dateOfBirth"
                  :state="errors.dateOfBirth"
                  @getValueControl="getValueControl"
                  @resetError="resetError('dateOfBirth')"
                  ></MISAInputDate>
                  <div class="error-mess" :title="errors.dateOfBirth" v-if="errors.dateOfBirth">
                    {{ errors.dateOfBirth }}
                  </div>
                </div>
                <div class="w-3/5">
                  <div class="input-wrapper p-l-10">
                    <div for="" class="label-input">Giới tính</div>
                    <div
                      SetField="gender"
                      class="input-container radio-wrapper"
                    >
                      <label
                        class="radio-container form-radio flex align-item-center"
                      >
                        <input
                          tabindex="4"
                          type="radio"
                          value="1"
                          v-model="employee.gender"
                          name="gender"
                        />
                        <span class="radio-checkmark">
                          <span class="radio-dot"></span>
                        </span>
                        <span class="radio-label">Nam</span>
                      </label>
                      <label
                        class="radio-container form-radio flex align-item-center"
                      >
                        <input
                          type="radio"
                          value="0"
                          v-model="employee.gender"
                          name="gender"
                        />
                        <span class="radio-checkmark">
                          <span class="radio-dot"></span>
                        </span>
                        <span class="radio-label">Nữ</span>
                      </label>
                      <label
                        class="radio-container form-radio flex align-item-center"
                      >
                        <input
                          type="radio"
                          value="2"
                          v-model="employee.gender"
                          name="gender"
                        />
                        <span class="radio-checkmark">
                          <span class="radio-dot"></span>
                        </span>
                        <span class="radio-label">Khác</span>
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="flex row-input">
                <div class="w-3/5 p-r-6">
                  <div class="input-wrapper">
                    <div for="" title="Số chứng minh nhân dân" class="label-input">Số CMND</div>
                    <div class="input-container">
                      <input
                        v-model="employee.identityNumber"
                        tabindex="6"
                        class="ms-input"
                        type="text"
                        lang="vi"
                      />
                    </div>
                  </div>
                </div>
                <div class="w-2/5">
                  <div class="input-wrapper">
                    <div for="" class="label-input">Ngày cấp</div>
                    <MISAInputDate 
                    tabindex="6"
                    property="identityIssuedDate" 
                    :value="employee.identityIssuedDate"
                    :state="errors.identityIssuedDate"
                    @getValueControl="getValueControl"
                    @resetError="resetError('identityIssuedDate')"
                    ></MISAInputDate>
                    <div class="error-mess" :title="errors.identityIssuedDate" v-if="errors.identityIssuedDate">
                      {{ errors.identityIssuedDate }}
                    </div>
                  </div>
                </div>
              </div>
              <div class="input-wrapper">
                <div for="" class="label-input">Nơi cấp</div>
                <div class="input-container">
                  <input
                    v-model="employee.identityIssuedPlace"
                    tabindex="9"
                    class="ms-input"
                    type="text"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="input-wrapper">
            <div for="" class="label-input">Địa chỉ</div>
            <div class="input-container">
              <input
                v-model="employee.address"
                tabindex="10"
                class="ms-input"
                type="text"
              />
            </div>
          </div>
          <div class="flex row-input">
            <div class="w-1/4 p-r-6">
              <div class="input-wrapper">
                <div for="" title="Điện thoại di động" class="label-input">ĐT di động</div>
                <div class="input-container">
                  <input
                    v-model="employee.phoneNumber"
                    tabindex="11"
                    class="ms-input"
                    type="text"
                  />
                </div>
              </div>
            </div>
            <div class="w-1/4 p-r-6">
              <div class="input-wrapper">
                <div for="" title="Điện thoại cố định" class="label-input">ĐT cố định</div>
                <div class="input-container">
                  <input
                    v-model="employee.telephoneNumber"
                    tabindex="11"
                    class="ms-input"
                    type="text"
                  />
                </div>
              </div>
            </div>
            <div class="w-1/4">
              <div class="input-wrapper">
                <div for="" class="label-input">Email</div>
                <div class="input-container">
                  <input
                    v-model="employee.email"
                    tabindex="12"
                    class="ms-input"
                    type="text"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="flex row-input">
            <div class="w-1/4 p-r-6">
              <div class="input-wrapper m-b-0">
                <div for="" class="label-input">Tài khoản ngân hàng</div>
                <div class="input-container m-b-0">
                  <input
                    v-model="employee.bankAccountNumber"
                    tabindex="13"
                    class="ms-input"
                    type="text"
                  />
                </div>
              </div>
            </div>
            <div class="w-1/4 p-r-6">
              <div class="input-wrapper m-b-0">
                <div for="" class="label-input">Tên ngân hàng</div>
                <div class="input-container m-b-0">
                  <input
                    v-model="employee.bankName"
                    tabindex="14"
                    class="ms-input"
                    type="text"
                  />
                </div>
              </div>
            </div>
            <div class="w-1/4">
              <div class="input-wrapper m-b-0">
                <div for="" class="label-input">Chi nhánh</div>
                <div class="input-container">
                  <input
                    v-model="employee.bankBranchName"
                    tabindex="15"
                    class="ms-input"
                    type="text"
                    @blur="tabOrderReset"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="form-footer">
        <div class="form-footer-left">
          <MISAButton
            @click="this.$emit('closeForm')"
            type="btn-sub"
            text="Hủy"
          ></MISAButton>
        </div>
        <div class="form-footer-right">
          <MISAButton
            @click="onSaveButton(Enum.FormAction.SaveAndClose)"
            type="btn-sub"
            text="Cất"
            class="m-r-8"
          ></MISAButton>
          <MISAButton
            @click="onSaveButton(Enum.FormAction.SaveAndAdd)"
            text="Cất và thêm"
          ></MISAButton>
        </div>
      </div>
      <div class="form-close">
        <v-tooltip text="Trợ giúp" location="bottom">
          <template v-slot:activator="{ props }">
            <div
              v-bind="props"
              class="ms-icon icon24 icon-help m-r-6 has-tooltip"
            ></div>
          </template>
        </v-tooltip>
        <v-tooltip text="Đóng" location="bottom">
          <template v-slot:activator="{ props }">
            <div
              v-bind="props"
              @click="onClickX"
              class="ms-icon icon24 icon-close has-tooltip"
            ></div>
          </template>
        </v-tooltip>
      </div>
    </div>
  </div>
</template>
<script>
import MISAButton from "@/components/base/MISAButton.vue";
import MISADropdown from "@/components/input/MISADropdown.vue";
import MISAInputDate from "@/components/input/MISAInputDate.vue";
import Resource from "@/js/Resources";
import Enum from "@/js/Enums";
export default {
  name: "EmployeeDetail",
  components: { MISAButton, MISADropdown, MISAInputDate },
  props: ["employeeSelected", "formMode"],
  data() {
    return {
      Enum,
      Resource,
      employee: {},
      oldEmployee: {},
      formEmployeeMode: this.formMode, 
      departments: [],
      employeeData: this.employeeSelected,
      isError: false,
      errors: {
        employeeCode: "",
        employeeName: "",
        departmentID: "",
        dateOfBirth: "",
        indentityDate: "",
      },
      serverErrors: [],
    };
  },

  created() {
    /**
     * Lấy dữ liệu đơn vị
     * NTDUONg
     */
    this.loadDepartment();
  },

  mounted() {
    try {
      this.$refs.empCodeInput.focus();
    } catch (error) {
      console.log(error);
    }
  },

  computed: {
    returnFormTitle(){
      if((this.formEmployeeMode == Enum.FormMode.Add) || (this.formEmployeeMode == Enum.FormMode.Duplicate)){
        return Resource.FormTitle.Add;
      }
      else{
        return Resource.FormTitle.Edit
      }
    }
  },

  methods: {
    /**
     * Hàm lấy dữ liệu department
     * NTDUONG
     */
    loadDepartment() {
      try {
        this.axios
          .get(Resource.APIs.Departments)
          .then((res) => {
            this.departments = res.data;
            // Sau khi có dữ liệu department, check form mode để hiện thị
            if ((this.formMode == Enum.FormMode.Edit) || (this.formMode == Enum.FormMode.Duplicate)) {
              // form mode là sửa-> gọi api lấy nhân viên theo id
              this.getEmpByID();
            } else {
              // form mode là thêm -> gọi api lấy mã nhân viên mới
              this.getNewEmployeeCode();
            }
          });
      } catch (error) {
        error;
      }
    },

    /**
     * Hàm lấy nhân viên theo id và bind lên form
     * NTDUONG
     */
    getEmpByID() {
      try {
        if (this.employeeSelected.employeeID) {
          this.axios
            .get(
              `${Resource.APIs.Employees}/${this.employeeSelected.employeeID}`
            )
            .then((res) => {
              // gán dữ liệu nhân viên vào object employee
              this.employee = res.data;
               // gán dữ liệu cho object oldEmployee
              this.oldEmployee = JSON.parse(JSON.stringify(this.employee));
              let departmentName;
              let departmentID = this.employee.departmentID;
              this.departments.forEach((item) => {
                if (item.departmentID == departmentID) {
                  departmentName = item.departmentName;
                }
              });
              // form mode là nhân bản -> gọi api lấy mã nhân viên mới
              if(this.formMode == Enum.FormMode.Duplicate){
                this.getNewEmployeeCode();
              }
              // set value cho drop down đơn vị
              this.$refs.departmentDropdown.setDropdownValue(
                departmentID,
                departmentName
              );
            });
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm lấy mã nhân viên mới
     * NTDUONG
     */
    getNewEmployeeCode() {
      try {
        this.axios
          .get(`${Resource.APIs.Employees}/new-code`)
          .then((res) => {
            this.employee.employeeCode = res.data;
             // gán dữ liệu cho object oldEmployee
            this.oldEmployee = JSON.parse(JSON.stringify(this.employee));
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm check các trường bắt buộc
     */
    checkBlurRequied(field) {
      try {
        if (!this.employee[field]) {
          switch (field) {
            case "employeeCode":
              this.errors[field] = Resource.FieldError.EmployeeCodeRequired;
              break;
            case "employeeName":
              this.errors[field] = Resource.FieldError.EmployeeNameRequired;
              break;
            case "departmentID":
              this.errors[field] = Resource.FieldError.DepartmentIdRequired;
              break;
          }
        } else {
          this.errors[field] = "";
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm resest lại input khi focus vào
     * NTDUONG
     */
    resetError(field) {
      try {
        this.errors[field] = "";
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm lấy giá trị của input date, gán vào object
     * NTDUONG
     */
     getValueControl(property, dateValue){
      try {
        this.employee[property] = dateValue;
      } catch (error) {
        console.log(error);
      }
     },

    /**
     * Hàm xử lí event click nút X trong form
     * NTDUONG
     */
    onClickX() {
      try {
        // Nếu dữ liệu trên form thay đổi so với ban đầu, show dialog confirm cho người dùng
        if(JSON.stringify(this.oldEmployee) != JSON.stringify(this.employee)){
          this.$emit("showQuestionDialog");
        }else{
          this.$emit('closeForm')
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm lưu dữ liệu và đóng form
     * NTDUONG
     */
    onSaveButton(action) {
      try {
        // validate dữ liệu các trường
        this.validateData();

        // lấy value của tất cả thuộc tính trong object errors, trả về true nếu không có value, false nếu có
        let isEmpty = Object.values(this.errors).every((value) => {
          if (!value) {
            return true;
          }
          return false;
        });
        // isEmpty == false -> show dialog báo lỗi
        if (!isEmpty) {
          this.$emit(
            "showErrorDialog",
            Object.values(this.errors),
            "iconError"
          );
          return;
        }

        // validate hợp lệ
        if (this.formEmployeeMode == Enum.FormMode.Edit) {
          // gọi api thêm dữ liệu nhân viên
          this.editData(action);
        } else {
          this.addData(action);
        }
      } catch (error) {
        console.log(error);
      }
    },
    

    /**
     * Hàm gọi api thêm dữ liệu nhân viên
     * NTDUONG
     */
    addData(action) {
      try {
        let context = this;
        let gender = parseInt(this.employee.gender);

        // deep copy từ data employee sang object data
        let data = JSON.parse(JSON.stringify(this.employee));
        data.gender = gender;
        delete data.employeeID;

        //gọi api thêm bằng axios
        this.axios
          .post(Resource.APIs.Employees, data)
          .then(function (response) {
            console.log(response);
            // hiển thị toast message thông báo thành công
            context.$emit("showToastMess", Resource.ToastMess.AddEmployeeSuccess);
            // emit lên cha gọi hàm đóng form và load lại dữ liệu
            if (action == Enum.FormAction.SaveAndClose) {
              context.$emit("closeFormAndReload");
            }
            if (action == Enum.FormAction.SaveAndAdd) {
              context.resetFormDetail();
            }
          })
          .catch(function (error) {
            context.handleErrorAPI(error);
          });
        // reset giá trị mảng lỗi từ server trả về
        this.serverErrors.length = 0;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm gọi api update dữ liệu nhân viên
     * NTDUONG
     */
    editData(action) {
      try {
        let context = this;
        let gender = parseInt(this.employee.gender);

        // deep copy từ data employee sang object data
        if(this.employee.dateOfBirth instanceof Date){
          this.employee.dateOfBirth.setHours(12);
        }
        if (this.employee.indentityIssuedDate instanceof Date) {
          this.employee.identityDate.setHours(12);
        }
        let data = JSON.parse(JSON.stringify(this.employee)),
        empId = data.employeeID;
        data.gender = gender;
        delete data.employeeID;

        //gọi api sửa bằng axios
        this.axios
          .put(`${Resource.APIs.Employees}/${empId}`, data)
          .then(function (response) {
            console.log(response);
            context.handleResponeAPI(action, Resource.ToastMess.EditEmployeeSuccess);
          })
          .catch(function (error) {
            context.handleErrorAPI(error);
          });
        // reset giá trị mảng lỗi từ server trả về
        this.serverErrors.length = 0;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xử lý respone từ API
     * NTDUONG
     */
    handleResponeAPI(action, toastMess){
      try {
        // hiển thị toast message thông báo thành công
        this.$emit("showToastMess", toastMess);
        // emit lên cha gọi hàm đóng form và load lại dữ liệu
        if (action == Enum.FormAction.SaveAndClose) {
          // nếu action là Cất -> tiến hành đóng form và reload lại dữ liệu
          this.$emit("closeFormAndReload");
        }
        if (action == Enum.FormAction.SaveAndAdd) {
          // Nếu action là Cất và Thêm -> tiến hành reset form, chyển form mode sang add và lấy mã nv mới
          this.resetFormDetail();
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xử lý lỗi trả về từ server
     * NTDUONG
     */
    handleErrorAPI(error){
      try {
        if(error.response.data.errorCode == 1){
          // gán message lỗi cho mảng lỗi từ server
          this.serverErrors = error.response.data.moreInfo;
          console.log(this.serverErrors);
          // truyền mảng lỗi và loại icon đến dialog thông báo lỗi
          this.$emit("showErrorDialog",this.serverErrors,"iconWarning");
        }
        if(error.response.data.errorCode == 2){
          // gán message lỗi cho mảng lỗi từ server
          this.serverErrors.push(error.response.data.userMsg);
          console.log(this.serverErrors);
          // truyền mảng lỗi và loại icon đến dialog thông báo lỗi
          this.$emit("showErrorDialog",this.serverErrors,"iconWarning");
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm reset form detail
     * NTDUONG
     */
    resetFormDetail(){
      try {
        this.$emit("reloadData");
        this.employee = {};
        this.$refs.departmentDropdown.setDropdownValue(null, null);
        this.formEmployeeMode = Enum.FormMode.Add;
        this.getNewEmployeeCode();
        this.$refs.empCodeInput.focus();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm lấy dữ liệu từ dropdown ra form
     * NTDUONG
     */
    getDropdownData() {
      try {
        let departmentID = this.$refs.departmentDropdown.departmentID;
        this.employee.departmentID = departmentID;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm validate dữ liệu
     * NTDUONG
     */
    validateData() {
      try {
        this.errors = {
          employeeCode: "",
          employeeName: "",
          departmentID: "",
          dateOfBirth: "",
          indentityDate: "",
        };
        this.validataRequired();
        this.validateDate();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm validate các trường bắt buộc
     * NTDUONG
     */
    validataRequired() {
      try {
        if (!this.employee.employeeCode) {
          this.errors.employeeCode = Resource.FieldError.EmployeeCodeRequired;
        }

        if (!this.employee.employeeName) {
          this.errors.employeeName = Resource.FieldError.EmployeeNameRequired;
        }

        if (!this.employee.departmentID) {
          this.errors.departmentID = Resource.FieldError.DepartmentIdRequired;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm validate các trường ngày tháng
     * NTDUONG
     */
    validateDate() {
      try {
        let nowDate = new Date();
        // check ngày sinh lớn hơn ngày hiện tại
        if (this.employee.dateOfBirth) {
          if(this.employee.dateOfBirth > nowDate){
            this.errors.dateOfBirth = Resource.FieldError.DateOfBirthInvalid;
          }
        }
        // check ngày cấp lớn hơn ngày hiện tại
        if (this.employee.identityIssuedDate) {
          if(this.employee.identityIssuedDate > nowDate){
            this.errors.identityIssuedDate = Resource.FieldError.IdentityIssuedDateInvalid;
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm focus lại ô input đầu tiên
     * NTDUONG
     */
     tabOrderReset(){
      this.$refs.empCodeInput.focus();
     }
  },
};
</script>
<style lang="scss">
@import "../../css/base/form.scss";
</style>
