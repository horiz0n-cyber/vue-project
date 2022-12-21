<template>
    <div class="content">
    <!-- Vùng page header -->
    <div class="page-header">
      <div class="page-header-title">Nhân viên</div>
      <MISAButton @click="openForm({}, Enum.FormMode.Add)" text="Thêm mới nhân viên"></MISAButton>
    </div>
    <!-- Vùng page body -->
    <div class="page-body">
      <!-- EmployeeToolbar -->
      <EmployeeToolbar
        @reloadTable="reloadData"
        @searchEmployee="searchEmployee"
        @clearIdArray="clearIdArray"
        :deleteIdArray="deleteArray"
        @handleDeleteMultiple="handleDeleteMultiple"
      />
      <!-- EmployeeTable -->
      <EmployeeTable
        ref="employeeGrid"
        :pageSize="recordsPerPage"
        :pageNumber="pageNumber"
        :employeeFilter="filterString"
        @onGetData="onGetData"
        @afterGetData="afterGetData"
        @openForm="openForm"
        @showDeleteWarning="showDeleteWarning"
        @getTotalRecordAndPage="getTotalRecordAndPage"
        @passDeleteArray="passDeleteArray"
      ></EmployeeTable>
      <!-- Các employee element position fixed -->
      <EmployeeFixedElements
        ref="employeeFixedElements"
        @reloadData="reloadData"
        @onDeleteRecord="onDeleteRecord"
      ></EmployeeFixedElements>
      <!-- Vùng page paging -->
      <MISAPaging
        ref="employeePaging"
        :totalRecord="totalRecord"
        :totalPage="totalPage"
        @setRecordsPerPage="setRecordsPerPage"
        @setPageIndex="setPageIndex"
      />
    </div>
  </div>
</template>
<script>
import EmployeeFixedElements from "./EmployeeFixedElements.vue";
import EmployeeTable from "./EmployeeTable.vue";
import EmployeeToolbar from "./EmployeeToolbar.vue";
import MISAPaging from "@/components/base/MISAPaging.vue";
import MISAButton from "@/components/base/MISAButton.vue";
import Enum from "@/js/Enums";
export default {
    name: "EmployeePage",
    components: { EmployeeTable, EmployeeToolbar, MISAPaging, MISAButton, EmployeeFixedElements },
    methods: {
        /**
         * Hàm emit lên cha để show loading
         * NTDUONG
         */
        onGetData() {
        try {
            this.$emit("onGetData");
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm emit lên cha để tắt loading
         * NTDUONG
         */
        afterGetData() {
        try {
            this.$emit("afterGetData");
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm mở form
         * NTDUONG
         * @param {đối tượng nhân viên} employee 
         * @param {trạng thái của form} formMode 
         */
        openForm(employee, formMode) {
        try {
            this.$refs.employeeFixedElements.openForm(employee, formMode);
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm lấy lại data
         * NTDUONG
         */
        reloadData() {
        try {
            this.$refs.employeeGrid.loadData();
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm gán tổng số bản ghi vào data để truyền tới prop MISAPaging
         * NTDUONG
         * @param {tổng số bản ghi} totalRecord
         */
        getTotalRecordAndPage(totalRecord, totalPage) {
        this.totalRecord = totalRecord;
        this.totalPage = totalPage;
        },

        /**
         * Hàm gọi emit show delete warning
         * NTDUONG
         */
        showDeleteWarning(data) {
        try {
            this.$refs.employeeFixedElements.showDeleteWarning(data);
        } catch (error) {
            console.log(error);
        }
        },
        
        /**
         * Hàm xử lí sau khi xóa thành công
         * NTDUONG
         */
        onDeleteRecord(){
            try {
                this.clearIdArray();
                this.reloadData();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm xử lí xóa hầng loạt
         * NTDUONG
         * @param {mảng id các bản ghi cần xóa} data 
         */
        handleDeleteMultiple(data){
        try {
            this.$refs.employeeFixedElements.showDeleteMultiple(data);
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm set số bản ghi trên 1 trang
         * NTDUONG
         */
        setRecordsPerPage(number) {
        try {
            this.recordsPerPage = number;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm set số trang truyền prop tới component EmployeeTable
         * NTDUONG
         */
        setPageIndex(index) {
        try {
            this.pageNumber = index;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm set chuỗi tìm kiếm để truyền prop đến component EmployeeTable
         * NTDUONG
         */
        searchEmployee(filterString) {
        try {
            this.$refs.employeePaging.pageIndex = 1;
            this.$refs.employeeGrid.filterString = filterString;
            this.$refs.employeeGrid.loadData();
            this.pageNumber = 1;
        } catch (error) {
            console.log(error);
        }
        },
        
        /**
         * Hàm truyền mảng id cần xóa từ component table
         * NTDUONG
         */
        passDeleteArray(deleteArray){
        try {
            this.deleteArray = deleteArray;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm clear mảng id nhân viên cần xóa
         * NTDUONG
         */
        clearIdArray(){
        try {
            this.$refs.employeeGrid.delteArrLength = 0;
            this.$refs.employeeGrid.checked = false;
            this.$refs.employeeGrid.deleteEmployeeIdArr = [];
        } catch (error) {
            console.log(error);
        }
        }
    },

    data() {
        return {
            Enum,
            totalRecord: 0,
            totalPage: 0,
            recordsPerPage: 20,
            deleteArray: [],
            pageNumber: 1,
            filterString: "",
        };
    },
}
</script>
<style lang="scss">
    @import "../../css/layout/page.scss";
</style>