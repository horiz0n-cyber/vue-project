<template>
  <div :class="['ms-icon', size, icon]"></div>
</template>
<script>
export default {
  name: "MISAIcon",
  props: ["size", "icon"],
};
</script>
<style lang="scss">
@import "../../css/base/icon.scss";
</style>
