<template>
  <!-- Vùng page toolbar -->
  <div class="page-toolbar">
    <div class="page-toolbar-left">
      <MISAMultiAction 
      :recordIdArray="deleteIdArray" 
      @clearIdArray="clearIdArray"
      @handleDeleteMultiple="handleDeleteMultiple"
      v-if="deleteIdArray.length"
      ref="multiAction"></MISAMultiAction>
    </div>
    <div class="page-toolbar-right">
      <div class="input-container page-toolbar-search">
        <input
          class="ms-input ms-input-icon input-search"
          v-model="filterString"
          @keyup.enter="searchEmployee"
          type="text"
          name=""
          id="Search"
          placeholder="Tìm theo mã, tên nhân viên"
        />
        <div class="icon icon20 input-icon">
          <MISAIcon @click="searchEmployee" size="icon16" icon="icon-search" />
        </div>
      </div>
      <v-tooltip text="Lấy lại dữ liệu" location="bottom">
        <template v-slot:activator="{ props }">
          <MISAIcon 
            v-bind="props"
            @click="this.$emit('reloadTable')"
            class="m-r-12"
            size="icon24" icon="icon-refresh"></MISAIcon>
        </template>
      </v-tooltip>
      <v-tooltip text="Xuất ra excel" location="bottom">
        <template v-slot:activator="{ props }">
          <MISAIcon 
          @click="exportToExcel"
          v-bind="props"
          size="icon24" icon="icon-excel"></MISAIcon>
        </template>
      </v-tooltip>
    </div>
  </div>
</template>
<script>
import MISAIcon from "@/components/base/MISAIcon";
import MISAMultiAction from "@/components/base/MISAMultiAction.vue";
import Resource from "@/js/Resources";
export default {
  name: "EmployeeToolbar",
  components: { MISAIcon, MISAMultiAction },
  props: ["deleteIdArray"],
  created() {
    console.log(this.deleteIdArray);
  },
  data() {
    return {
      filterString: "",
    };
  },

  methods: {
    /**
     * Hàm xử lí click/enter ô tìm kiếm nhân viên
     * NTDUONG
     */
    searchEmployee() {
      try {
        this.$emit("searchEmployee", this.filterString);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm export dữ liệu ra excel
     * NTDUONG
     */
     exportToExcel(){
      try {
        let filter = "";
        // Nếu input tìm kiếm có dữ liệu, gán vào filter để tạo param filterString cho url query
        if (this.filterString) {
          filter = `?filterString=${this.filterString}&`;
        }
        this.axios({
          url: `${Resource.APIs.Employees}/export${filter}`,
          method: "GET",
          responseType: 'blob'
        }).then(response => {
          var fileURL = window.URL.createObjectURL(new Blob([response.data]));
          var fileLink = document.createElement('a');
        
          fileLink.href = fileURL;
          fileLink.setAttribute('download', 'Danh_sach_nhan_vien.xlsx');
          document.body.appendChild(fileLink);
          fileLink.click();
          })
        .catch(error => {
          console.log(error);
        });
      } catch (error) {
        console.log(error);
      }
     },
     
     /**
      * Hàm emit lên cha để clear mảng id nhân viên đã chọn
      * NTDUONG
      */
      clearIdArray(){
        try {
          this.$emit("clearIdArray");
        } catch (error) {
          console.log(error);
        }
      },

      /**
       * Hàm emit lên cha, show dialog cảnh báo xóa hàng loạt 
       * NTDUONG
       */
       handleDeleteMultiple(data){
        try {
          this.$emit("handleDeleteMultiple", data);
        } catch (error) {
          console.log(error);
        }
       }
  },
};
</script>
<style lang="scss">
@import "../../css/layout/page.scss";
</style>
