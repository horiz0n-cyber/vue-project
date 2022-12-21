<template>
  <tr
    @click="selectRow"
    @dblclick="openEditForm(employee)"
    :class="{ 'selected-row': rowChecked }"
  >
    <td class="checkbox">
      <MISACheckbox :value="rowChecked" @handleChangeCheckbox="handleChangeCheckbox" />
    </td>
    <td>{{ employee.employeeCode }}</td>
    <td>{{ employee.employeeName }}</td>
    <td>{{ convertEnum(employee.gender, "Gender") }}</td>
    <td class="align-center">{{ formatDate(employee.dateOfBirth) }}</td>
    <td>{{ employee.identityNumber }}</td>
    <td>{{ employee.positionName }}</td>
    <td>{{ employee.departmentName }}</td>
    <td>{{ employee.bankAccountNumber }}</td>
    <td>{{ employee.bankName }}</td>
    <td>{{ employee.bankBranchName }}</td>
    <td class="table-tool">
      <div class="function flex">
        <button @click="openEditForm" class="btn-table p-r-0">
          Sửa
        </button>
        <button
          @click.stop="toggleContextMenu"
          ref="contextDropDown"
          class="btn-table"
        >
          <div class="ms-icon icon16 icon-dropdown-blue"></div>
        </button>
      </div>
    </td>
    <MISAContextMenu
      v-click-outside-element="closeContextMenu"
      v-if="isShowContextMenu"
      :data="employee"
      :style="{ top: contextMenuPostition + 'px' }"
      @showDeleteWarning="showDeleteWarning"
      @openDuplicateForm="openDuplicateForm"
    ></MISAContextMenu>
  </tr>
</template>
<script>
import MISACheckbox from "@/components/base/MISACheckbox";
import MISAContextMenu from "@/components/base/MISAContextMenu.vue";
import Enum from "@/js/Enums";
import Resource from "@/js/Resources";
export default {
  name: "EmployeeTableRow",
  components: { MISACheckbox, MISAContextMenu },
  methods: {
    /**
     * Hàm format ngày tháng trên bảng
     * NTDUONG
     * @param {chuỗi ngày tháng năm} dateSrc
     */
    formatDate(dateSrc) {
      try {
        if (dateSrc) {
          let date = new Date(dateSrc),
            day = date.getDate().toString().padStart(2, "0"),
            month = (date.getMonth() + 1).toString().padStart(2, "0"),
            year = date.getFullYear().toString();
          return `${day}/${month}/${year}`;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm convert enum sang string tương ứng
     * NTDUONG
     * @param {giá trị enum} data
     * @param {tên của prop Enum} enumName
     */
    convertEnum(data, enumName) {
      try {
        if (data != null) {
          // lấy object enumName trong Enum và Resource
          let enumeration = Enum[enumName],
              resource = Resource[enumName];
          // duyệt object Enum[enumName], nếu bằng data thì lấy string tương ứng trong Resource[enumName]
          for(let prop in enumeration){
              if(enumeration[prop] == data){
                  data = resource[prop];
              }
          }
          // trả về giá trị string tương ứng với enum
          return data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm bật tắt context menu
     * NTDUONG
     */
    toggleContextMenu() {
      try {
        // Lấy vị trí của nút toggle context menu
        this.contextMenuPostition =
          this.$refs.contextDropDown.getBoundingClientRect().top + 16;
        console.log(this.employee);
        this.isShowContextMenu = !this.isShowContextMenu;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm select 1 hàng trong bảng
     * NTDUONG
     */
    selectRow() {
      try {
        // Gọi emit lên cha EmployeeTable, truyền index của hàng
        this.$emit("onSelectRow", this.index);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm đóng context menu khi ấn ra ngoài
     * NTDUONG
     */
    closeContextMenu() {
      try {
        this.isShowContextMenu = false;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm mở form edit
     * NTDUONG
     */
    openEditForm() {
      try {
        //Gọi emit lên cha EmployeeTable
        this.$emit("openForm", this.employee, Enum.FormMode.Edit);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm mở form nhân bản
     */
    openDuplicateForm(){
      try {
        //Gọi emit lên cha EmployeeTable
        this.$emit("openForm", this.employee, Enum.FormMode.Duplicate);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm gọi emit show delete warning
     * NTDUONG
     */
    showDeleteWarning(data) {
      try {
        this.$emit("showDeleteWarning", data);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xử lí sự kiện thay đổi check box
     * NTUDONG
     */
    handleChangeCheckbox(){
      try {
        this.$emit("updateDeleteArray", this.employee.employeeID);
      } catch (error) {
        console.log(error);
      }
    }
  },
  props: ["employee", "index", "selected", "rowChecked"],

  data() {
    return {
      isShowContextMenu: false,
      contextMenuPostition: "",
    };
  },
};
</script>
<style>
@import "../../css/layout/grid.scss";
</style>
