<template>
    <div class="total-selected m-r-16">
        Đã chọn <span class="number-selected">{{ idArray.length }}</span>
    </div>
    <div @click="clearIdArray" class="btn-unselected m-r-16">
        Bỏ chọn
    </div>
    <MISAButton @click="handleDeleteMultiple" text="Xóa" type="btn-danger"></MISAButton>
</template>
<script>
import MISAButton from './MISAButton.vue';
export default {
    name: "MISAMultiAction",
    components: { MISAButton },
    props: ["recordIdArray"],
    data() {
        return {
            idArray: this.recordIdArray,
        }
    },
    methods: {
        /***
         * Hàm emit lên cha, clear mảng id cần xóa
         */
        clearIdArray(){
            try {
                this.$emit("clearIdArray");
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm gọi emit lên cha, show dialog cảnh báo xóa hàng loạt bản ghi 
         * NTDUONG
         */
         handleDeleteMultiple(){
            try {
                this.$emit("handleDeleteMultiple", this.idArray);
            } catch (error) {
                console.log(error);
            }
         }
    }
}
</script>
<style lang="scss"> 
    @import "../../css/layout/page.scss"
</style>