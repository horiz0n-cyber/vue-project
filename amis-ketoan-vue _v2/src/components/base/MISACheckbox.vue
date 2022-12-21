<template>
  <label class="checkbox-container flex align-item-center">
    <input :checked="value" type="checkbox" @change="handleChangeCheckbox" />
    <span class="checkmark"></span>
  </label>
</template>
<script>
export default {
  name: "MISACheckbox",
  props: ["value"],
  methods: {
    handleChangeCheckbox(){
      this.$emit("handleChangeCheckbox");
    }
  }
};
</script>
<style lang="scss">
@import "../../css/base/checkbox.scss";
</style>
