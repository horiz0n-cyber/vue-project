<template>
    <EmployeeDetail
    v-if="isShowForm"
    ref="employeeDetail"
    :formMode="formMode"
    :employeeSelected="employeeSelected"
    @closeForm="closeForm"
    @showQuestionDialog="showQuestionDialog"
    @showErrorDialog="showErrorDialog"
    @reloadData="reloadData"
    @closeFormAndReload="closeFormAndReLoad"
    @showToastMess="showToastMess"
  ></EmployeeDetail>
  <MISADialog
    v-if="isShowDialog"
    :dialogMode="dialogMode"
    :icon="iconType"
    :data="employeeSelected"
    :dataArr="idArray"
    :errors="formErrors"
    @closeDialog="closeDialog"
    @closeForm="closeForm"
    @saveData="saveDataForm"
    @onDeleteRecord="onDeleteRecord"
  ></MISADialog>
  <Transition name="slide-fade">
    <MISAToastMess v-if="isShowToastMess" :toastContent="toastContent" @closeToastMess="closeToastMess"></MISAToastMess>
  </Transition>
</template>
<script>
import EmployeeDetail from "./EmployeeDetail.vue";
import MISADialog from "@/components/base/MISADialog.vue";
import MISAToastMess from "@/components/base/MISAToastMess.vue";
import Enum from "@/js/Enums";
export default {
    name: "EmployeeFixedElements",
    components: {
        EmployeeDetail,
        MISADialog,
        MISAToastMess
    },
    data() {
        return {
            isShowForm: false,
            isShowDialog: false,
            isShowToastMess: false,
            employeeSelected: {},
            idArray: [],
            formErrors: [],
            iconType: "",
            dialogMode: "",
            formMode: 0,
            toastContent: "",  
        }
    },
    methods: {
        /**
         * Hàm mở form
         * NTDUONG
         */
        openForm(employee, formMode) {
        try {
            this.employeeSelected = employee;
            console.log(this.employeeSelected);
            this.formMode = formMode;
            this.isShowForm = true;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm đóng form
         * NTDUONG
         */
        closeForm() {
        this.isShowForm = false;
        },

        /**
         * Hàm show dialog cảnh báo xóa
         * NTDUONG
         * @param {dữ liệu employee cần xóa} dialogMode
         */
        showDeleteWarning(data) {
        try {
            this.dialogMode = Enum.DialogMode.Warning;
            this.employeeSelected = data;
            this.isShowDialog = true;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm hiển thị dialog cảnh báo xóa hàng loạt
         */
        showDeleteMultiple(data){
        try {
            this.dialogMode = Enum.DialogMode.WarningMulti;
            this.idArray = data;
            this.isShowDialog = true;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm show question dialog
         * NTDUONG
         */
        showQuestionDialog() {
        try {
            this.dialogMode = Enum.DialogMode.Question;
            this.isShowDialog = true;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm show error dialog
         * NTDUONG
         */
        showErrorDialog(formErrors, iconType) {
        try {
            // gán dialog mode
            this.dialogMode = Enum.DialogMode.Error;
            // gán mảng các lỗi của form
            this.formErrors = formErrors;
            //gán kiểu icon cho form form
            this.iconType = iconType;
            this.isShowDialog = true;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm đóng dialog
         * NTDUONG
         */
        closeDialog() {
        try {
            this.isShowDialog = false;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm xử lí sau khi xóa thành công
         * NTDUONG
         */
        onDeleteRecord(message){
        try {
            this.$emit("onDeleteRecord");
            this.showToastMess(message);
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm hiển thị toast message
         * NTDUONG
         */
        showToastMess(message){
        try {
            this.toastContent = message;
            this.isShowToastMess = true;
            // tự đóng toast message sau 5s
            setTimeout(() => {
            this.closeToastMess()
            }, 5000);
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm đóng toast mess 
         * NTDUONG
         */
        closeToastMess(){
        try {
            this.isShowToastMess = false;
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm reload trang
         * NTDUONG
         */
        reloadData() {
        try {
            this.$emit("reloadData");
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm đóng form và reload trang
         * NTDUONG
         */
        closeFormAndReLoad() {
        try {
            this.closeForm();
            this.reloadData();
        } catch (error) {
            console.log(error);
        }
        },

        /**
         * Hàm gọi đến hàm lưu dữ liệu của component EmlpoyeeDetail
         * NTDUONG
         */
        saveDataForm(action) {
        try {
            this.$refs.employeeDetail.onSaveButton(action);
        } catch (error) {
            console.log(error);
        }
        },
    }

}
</script>
<style lang="scss">
    @import "../../css/main.scss";
</style>