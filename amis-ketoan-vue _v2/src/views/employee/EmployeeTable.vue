<template>
  <!-- Vùng page table -->
  <div id="gridEmployee" class="page-table" >
    <table>
      <thead>
        <tr>
          <th class="checkbox" style="background-color: #eeeeee">
            <MISACheckbox ref="misaCheckbox" :value="checked" @handleChangeCheckbox="handleChangeCheckbox"/>
          </th>
          <th>MÃ NHÂN VIÊN</th>
          <th>TÊN NHÂN VIÊN</th>
          <th>GIỚI TÍNH</th>
          <th class="align-center">NGÀY SINH</th>
          <th>SỐ CMND</th>
          <th>CHỨC DANH</th>
          <th>TÊN ĐƠN VỊ</th>
          <th>SỐ TÀI KHOẢN</th>
          <th>TÊN NGÂN HÀNG</th>
          <th>CHI NHÁNH NGÂN HÀNG</th>
          <th class="table-tool" style="background-color: #eeeeee">
            CHỨC NĂNG
          </th>
        </tr>
      </thead>
      <tbody>
        <EmployeeTableRow
          v-for="(employee, index) in employees"
          :key="index"
          :index="index"
          :employee="employee"
          :rowChecked="rowCheckState(employee.employeeID)"
          :selected="index == rowSelected ? true : false"
          @onSelectRow="selectRow"
          @openForm="openForm"
          @showDeleteWarning="showDeleteWarning"
          @updateDeleteArray="updateDeleteArray"
        ></EmployeeTableRow>
      </tbody>
    </table>
  </div>
</template>
<script>
import MISACheckbox from "@/components/base/MISACheckbox";
import EmployeeTableRow from "./EmployeeTableRow.vue";
import Resource from "../../js/Resources.js"
export default {
  name: "EmployeeTable",
  components: { MISACheckbox, EmployeeTableRow },
  props: ["pageSize", "pageNumber"],
  created() {
    this.loadData();
  },
  data() {
    return {
      employees: [],
      deleteEmployeeIdArr: [],
      delteArrLength: 0,
      checked: false,
      rowSelected: null,
      totalRecord: 0,
      totalPage: 0,
      filterString: "",
    };
  },

  watch: {
    // theo dõi sự thay đổi của pageSize, nếu thay đổi load lại Data
    pageSize() {
      this.loadData();
    },

    // Theo dõi thay đỏi của page Number, nếu có load lại data
    pageNumber() {
      this.loadData();
    },

    // theo dõi sự thay đổi của mảng nhân viên. nễu thay đổi -> check xem mảng id cần xóa có chứa toàn bộ 
    // id của mảng nhân viên mới không
    employees(){
      try {
        for(let employee of this.employees){
          if(!this.deleteEmployeeIdArr.includes(employee.employeeID)){
            this.checked = false;
            return;
          }
        }
        this.checked = true;
      } catch (error) {
        console.log(error);
      }
    },

    delteArrLength(){
      this.$emit("passDeleteArray", this.deleteEmployeeIdArr);
    }
  },

  methods: {

    /**
     * Hàm trả về trạng thái của hàng có tick hay không
     * @param {id nhân viên của hàng cần check} employeeID 
     */
    rowCheckState(employeeID){
      try {
        return this.deleteEmployeeIdArr.includes(employeeID);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm load dữ liệu vào bảng
     * NTDUONG
     */
    loadData() {
      try {
        // Load dữ liệu
        this.$emit("onGetData");

        let filter = "";
        // Nếu input tìm kiếm có dữ liệu, gán vào filter để tạo param filterString cho url query
        if (this.filterString) {
          filter = `filterString=${this.filterString}&`;
        }
        this.axios
          .get(
            `${Resource.APIs.Employees}/filter?${filter}pageSize=${this.pageSize}&pageNumber=${this.pageNumber || 1}`
          )
          .then((res) => {
            if (res  && res.data && res.data.data) {
              // gán dữ liệu vào mảng của component data
              this.employees = res.data.data;
              console.log(res);
              // Lấy dữ liệu tổng số bản ghi
              if (res.data.totalRecord) {
                this.totalRecord = res.data.totalRecord;
              } else {
                this.totalRecord = 0;
              }
              // Tính tổng số trang
              if((this.totalRecord % this.pageSize) == 0){
                this.totalPage = Math.floor(this.totalRecord / this.pageSize) ;
              }else{
                this.totalPage = Math.floor(this.totalRecord / this.pageSize) + 1;
              }
              this.$emit(
                "getTotalRecordAndPage",
                this.totalRecord,
                this.totalPage
              );
            }else{
              // Thông báo lỗi cho người dùng
              console.log("Có lỗi xảy ra khi lấy dữ liệu");

              this.employees={};
            }
            // Tắt loading
            this.$emit("afterGetData");
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm thêm clas vào hàng được select
     * NTDUONG
     * @param {index của hàng vừa click} index
     */
    selectRow(index) {
      try {
        // Gán index của hàng vừa click
        this.rowSelected = index;
      } catch (error) {
        console.log(error);
      }
    },

    /***
     * Hàm mở emit lên gọi hàm mở form 
     * NTDUONG
     */
    openForm(employee, formMode) {
      try {
        this.$emit("openForm", employee, formMode);
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
     * Hàm xử lí sự kiện thay đổi checkbox
     */
    handleChangeCheckbox(){
      try {
        if(this.checked){
          // xử lí khi bỏ tick check tất cả các hàng
          for(let employee of this.employees){
            // nếu trong mảng id cần xóa có tồn tại employeeID, xóa id đó khỏi mảng
            if(this.deleteEmployeeIdArr.includes(employee.employeeID)){
              let index = this.deleteEmployeeIdArr.indexOf(employee.employeeID);
              this.deleteEmployeeIdArr.splice(index, 1);
            }
          }
          this.checked = false;
        }else{
          // xử lí khi tick check tất cả các hàng
          for(let employee of this.employees){
            // nếu trong mảng id cần xóa không tồn tại employeeID, push id đó vào mảng
            if(!this.deleteEmployeeIdArr.includes(employee.employeeID)){
              this.deleteEmployeeIdArr.push(employee.employeeID);
            }
          }
          this.checked = true;
        }
        this.delteArrLength = this.deleteEmployeeIdArr.length;
      } catch (error) {
        console.log(error);
      }
    },
    
    /**
     * Hàm update mảng delete từ table row
     * NTDUONG
     */
    updateDeleteArray(employeeID){
      try {
        if(this.deleteEmployeeIdArr.includes(employeeID)){
          let index = this.deleteEmployeeIdArr.indexOf(employeeID);
          this.deleteEmployeeIdArr.splice(index, 1);
        }else{
          this.deleteEmployeeIdArr.push(employeeID);
        }
        this.delteArrLength = this.deleteEmployeeIdArr.length;
      } catch (error) {
        console.log(error);
      }
    }

  },
};
</script>
<style lang="scss">
@import "../../css/layout/grid.scss";
</style>
