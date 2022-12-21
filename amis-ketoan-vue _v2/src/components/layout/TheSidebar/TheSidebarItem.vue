<template>
  <v-tooltip
    location="end top"
    origin="top center"
    :text="sidebarItem.title"
    color="#616161"
    z-index="200"
  >
    <template v-slot:activator="{ props }">
      <!-- Sidebar menu item -->
      <div v-bind="props" class="parent-menu-item">
        <router-link :to="sidebarItem.route"
          href=""
          Tooltip="Tổng quan"
          class="sidebar-menu-item has-tooltip"
          :class="{ 'menu-item-active': isActive }"
        >
          <div class="menu-item-icon">
            <MISAIcon :icon="sidebarItem.icon" size="icon24"></MISAIcon>
          </div>
          <div class="menu-item-title">{{ sidebarItem.title }}</div>
        </router-link>
      </div>
    </template>
  </v-tooltip>
</template>

<script>
import MISAIcon from "@/components/base/MISAIcon";
export default {
  name: "MISASidebarItem",
  components: { MISAIcon },
  props: ["sidebarItem", "isActive"],
};
</script>
<style lang="scss">
@import "../../../css/layout/sidebar.scss";
</style>
