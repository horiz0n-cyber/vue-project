<template>
  <template v-if="dialogMode == Enum.DialogMode.Question">
    <!-- dialog cảnh báo  -->
    <div id="EmployeeConfirmDialog" class="dialog-container">
      <div class="dialog">
        <div class="dialog-body">
          <MISAIcon size="icon48" icon="icon-question"></MISAIcon>
          <div class="dialog-message">
            Dữ liệu đã bị thay đổi, bạn có muốn cất không?
          </div>
        </div>
        <div class="dialog-footer flex">
          <div class="dialog-footer-left">
            <MISAButton
              @click="closeDialog"
              type="btn-sub"
              text="Hủy"
            ></MISAButton>
          </div>
          <div class="dialog-footer-right">
            <MISAButton
              @click="closeDialogAndForm"
              type="btn-sub"
              text="Đóng"
              class="m-r-8"
              >Không</MISAButton
            >
            <MISAButton @click="closeDialogAndSave" text="Có"></MISAButton>
          </div>
        </div>
      </div>
    </div>
  </template>
  <template v-if="(dialogMode == Enum.DialogMode.Error)">
    <!-- dialog thông báo lỗi -->
    <div id="EmployeeErrorDialog" class="dialog-container">
      <div class="dialog">
        <div class="dialog-body">
          <MISAIcon
            size="icon48"
            :icon="icon == 'iconWarning' ? 'icon-warning' : 'icon-error'"
          ></MISAIcon>
          <div class="dialog-mess-wrapper">
            <div v-for="(error,index) in errors" :key="index" class="dialog-message">{{ error }}</div>
          </div>
        </div>
        <div
          class="dialog-footer flex align-item-center justify-content-end"
        >
          <MISAButton
            @click="this.$emit('closeDialog')"
            text="Đóng"
          ></MISAButton>
        </div>
      </div>
    </div>
  </template>
  <template v-if='dialogMode == Enum.DialogMode.Warning'>
    <!-- dialog cảnh báo xóa  -->
    <div id="EmployeeConfirmDeleteDialog" class="dialog-container">
      <div class="dialog">
        <div class="dialog-body">
          <MISAIcon size="icon48" icon="icon-warning"></MISAIcon>
          <div class="dialog-mess-wrapper"> 
            <div class="dialog-message">
              Bạn có thực sự muốn xóa nhân viên {{ data.employeeCode }} không ?
            </div>
          </div>
        </div>
        <div class="dialog-footer flex">
          <div class="dialog-footer-left">
            <MISAButton
              @click="this.$emit('closeDialog')"
              type="btn-sub"
              text="Không"
            ></MISAButton>
          </div>
          <div class="dialog-footer-right">
            <MISAButton @click="deleteRecord" type="btn-danger" text="Có"></MISAButton>
          </div>
        </div>
      </div>
    </div>
  </template>
  <!-- dialog cảnh báo xóa hàng loạt -->
  <template v-if='dialogMode == Enum.DialogMode.WarningMulti'>
    <div id="EmployeeConfirmDeleteDialog" class="dialog-container">
      <div class="dialog">
        <div class="dialog-body">
          <MISAIcon size="icon48" icon="icon-warning"></MISAIcon>
          <div class="dialog-mess-wrapper">
            <div class="dialog-message">
              Bạn có thực sự muốn xóa những nhân viên đã chọn không ?
            </div>
          </div>
        </div>
        <div class="dialog-footer flex">
          <div class="dialog-footer-left">
            <MISAButton
              @click="this.$emit('closeDialog')"
              type="btn-sub"
              text="Không"
            ></MISAButton>
          </div>
          <div class="dialog-footer-right">
            <MISAButton @click="deleteMultiRecord" type="btn-danger" text="Có"></MISAButton>
          </div>
        </div>
      </div>
    </div>
  </template>
</template>

<script>
import MISAButton from "./MISAButton.vue";
import MISAIcon from "./MISAIcon";
import Resource from "@/js/Resources";
import Enum from '@/js/Enums';
export default {
  name: "MISADialog",
  components: { MISAIcon, MISAButton },
  props: ["dialogMode", "data", "errors", "icon", "dataArr"],
  data() {
    return {
      Enum,
      Resource,
    }
  },
  methods: {
    /**
     * Hàm đóng dialog
     * NTDUONG
     */
    closeDialog() {
      try {
        this.$emit("closeDialog");
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm đóng dialog và form
     * NTDUONG
     */
    closeDialogAndForm() {
      try {
        this.closeDialog();
        this.$emit("closeForm");
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm đóng dialog và lưu dữ liệu
     * NTDUONG
     */
    closeDialogAndSave() {
      try {
        this.closeDialog();
        this.$emit("saveData", Enum.FormAction.SaveAndClose);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm gọi api xóa bản ghi
     * NTDUONG
     */
    deleteRecord() {
      try {
        let context = this;
        this.axios
          .delete(
            `${Resource.APIs.Employees}/${this.data.employeeID}`
          )
          .then((res) => {
            console.log(res);
            // đóng dialog
            context.closeDialog();
            // emit lên cha để reload lại dữ liệu
            context.$emit("onDeleteRecord", Resource.ToastMess.DeleteEmployeeSuccess);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm gọi api xóa hàng loạt bản ghi
     * NTDUONG
     */
     deleteMultiRecord(){
      try {
        let context = this;
        this.axios
          .delete(Resource.APIs.Employees, {
            data: this.dataArr
          })
          .then((res) => {
            console.log(res);
            // đóng dialog
            context.closeDialog();
            // emit lên cha để reload lại dữ liệu
            context.$emit("onDeleteRecord", Resource.ToastMess.DeleteEmployeeSuccess);
          });
      } catch (error) {
        console.log(error);
      }
     }
  },
};
</script>
<style lang="scss">
@import "../../css/base/dialog.scss";
</style>
