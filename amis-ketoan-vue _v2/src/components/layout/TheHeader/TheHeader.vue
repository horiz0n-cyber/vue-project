<template>
  <div class="header">
    <div class="header-left">
      <MISAIcon size="icon24" :icon="iconToggle"></MISAIcon>
      <div class="header-company">CÔNG TY CỔ PHẦN MISA</div>
      <a class="" href="">
        <MISAIcon
          size="icon14"
          :icon="iconDown"
          class="company-selector"
        ></MISAIcon>
      </a>
    </div>
    <div class="header-right">
      <v-tooltip text="Thông báo" location="bottom">
        <template v-slot:activator="{ props }">
          <div
            v-bind="props"
            class="ms-icon icon24 icon-notify header-noti has-tooltip"
          ></div>
        </template>
      </v-tooltip>

      <button class="btn header-account">
        <div class="header-avatar">
          <img
            class=""
            src="../../../assets/img/PXL_20220210_012636569.PORTRAIT.jpg"
            alt=""
          />
        </div>
        <div class="header-username">Nguyễn Thái Dương</div>
        <div class="ms-icon icon14 icon-chevron-down"></div>
      </button>
    </div>
  </div>
</template>
<script>
import MISAIcon from "@/components/base/MISAIcon";
export default {
  name: "TheHeader",
  components: { MISAIcon },
  data() {
    return {
      iconToggle: "icon-toggle",
      iconDown: "icon-down",
    };
  },
};
</script>
<style lang="scss">
@import "../../../css/layout/header.scss";
</style>
