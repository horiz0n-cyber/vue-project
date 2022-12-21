<template>
    <div class="toast-mess-wrapper">
        <div class="toast-mess">
            <div class="toast-mess-left">
                <div class="icon icon24">
                    <MISAIcon icon="icon-success" size="icon20"/>
                </div>
                <div class="toast-mess-body"><span class="toast-mess-title">Thành công! </span>{{ toastContent }}</div>
            </div>
            <div class="toast-mess-right">
                <!-- <div class="toast-mess-action">Hoàn tác</div> -->
                <div @click='this.$emit("closeToastMess")' class="icon icon16">
                    <MISAIcon icon="icon-x" size="icon12"/>
                </div>
            </div>     
        </div>
    </div>
</template>
<script>
import MISAIcon from './MISAIcon';
export default {
    name: "MISAToastMess",
    components: { MISAIcon },
    props: ["toastContent"],
}
</script>
<style lang="scss">
    @import "../../css/base/toast-mess.scss";
</style>