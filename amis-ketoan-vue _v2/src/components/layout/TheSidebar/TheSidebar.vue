<template>
  <!-- Vùng vẽ sidebar -->
  <div class="sidebar">
    <div class="sidebar-header">
      <MISAIcon size="icon24" :icon="icon"></MISAIcon>
      <a href="" class="logo">
        <img
          class="logo"
          src="../../../assets/img/Logo_Module_TiengViet_White.66947422.svg"
          alt=""
        />
      </a>
    </div>
    <div class="sidebar-menu">
      <TheSidebarItem
        v-for="(sidebarItem, index) in sidebarItems"
        :key="index"
        :isActive="index === itemActive ? true : false"
        :sidebarItem="sidebarItem"
      ></TheSidebarItem>
    </div>
  </div>
</template>
<script>
import MISAIcon from "@/components/base/MISAIcon";
import TheSidebarItem from "./TheSidebarItem.vue";

export default {
  name: "TheSidebar",
  components: { TheSidebarItem, MISAIcon },
  data() {
    return {
      sidebarItems: [
        { icon: "icon-dashboard", title: "Tổng quan", route: "/" },
        { icon: "icon-moneybag", title: "Tiền mặt", route:"/employees" },
        { icon: "icon-bank", title: "Tiền gửi", route: "/customers"},
        { icon: "icon-pu", title: "Mua hàng", route: "/customers"},
        { icon: "icon-sale", title: "Bán hàng", route: "/customers" },
        { icon: "icon-invoice", title: "Quản lý hoá đơn", route: "/customers" },
        { icon: "icon-stock", title: "Kho", route: "/customers" },
        { icon: "icon-tools ", title: "Công cụ dụng cụ", route: "/customers" },
        { icon: "icon-fixed-assets", title: "Tài sản cố định", route: "/customers" },
        { icon: "icon-price", title: "Giá thành", route: "/customers" },
        { icon: "icon-general", title: "Tổng hợp", route: "/customers" },
        { icon: "icon-budget", title: "Ngân sách", route: "/customers" },
        { icon: "icon-report", title: "Báo cáo", route: "/customers" },
        { icon: "icon-finance", title: "Phân tích tài chính", route: "/customers" },
      ],
      icon: "icon-platform",
      itemActive: 1,
    };
  },
};
</script>
<style lang="scss">
@import "../../../css/layout/sidebar.scss";
</style>
