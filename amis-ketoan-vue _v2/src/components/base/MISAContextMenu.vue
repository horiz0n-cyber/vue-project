<template>
  <div class="table-context-menu">
    <div @click="dulicateRecord" class="context-menu-item">Nhân bản</div>
    <div @click="deleteRecord" class="context-menu-item">Xóa</div>
    <div class="context-menu-item">Ngừng sử dụng</div>
  </div>
</template>
<script>
import Enum from '@/js/Enums';
export default {
  name: "MISAContextMenu",
  props: ["data"],
  methods: {
    /**
     * Hàm gọi emit lên cha để hiển thị delete dialog
     * NTDUONG
     */
    deleteRecord() {
      try {
        this.$emit("showDeleteWarning", this.data);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm gọi emit lên cha để hiển thị form nhân bảnư
     * NTDUONG
     */
    dulicateRecord(){
      try {
        this.$emit("openDuplicateForm", this.data, Enum.FormMode.Duplicate);
      } catch (error) {
        console.log(error);
      }
    }
  },

  created() {},
};
</script>
<style lang="scss">
@import "../../css/layout/grid.scss";
</style>
