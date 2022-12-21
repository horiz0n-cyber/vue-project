<template>
  <button :class="['btn', type, state]">{{ text }}</button>
</template>
<script>
export default {
  name: "MISAButton",
  props: ["type", "state", "text"],
};
</script>
<style lang="scss">
@import "../../css/base/button.scss";
</style>
