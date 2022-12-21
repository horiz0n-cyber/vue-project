<template>
  <div class="input-container dropdown">
    <input
      :class="{ 'ms-input-error': state }"
      ref="inputDropdown"
      @focus="this.$emit('resetError')"
      @blur="this.$emit('checkRequied')"
      v-model="departmentName"
      :title="title"
      tabindex="5"
      class="ms-input ms-input-icon dropdown-input"
      type="text"
      readonly
    />
    <div class="icon btn-dropdown" @click="toggleDropdown">
      <div class="ms-icon icon16 icon-dropdown"></div>
    </div>
    <div v-show="isShowDropdownOption" class="dropdown-option-container">
      <div class="dropdown-option">
        <div
          class="dropdown-item"
          v-for="(item, index) in data"
          :key="index"
          :index="index"
          :class="{ selected: item.departmentID == departmentID }"
          @click="hanldeItemClick(item.departmentID, item.departmentName)">
          {{ item.departmentName }}
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISADropdown",
  props: ["state", "data", "itemID", "itemName", "title"],
  data() {
    return {
      departments: [],
      isShowDropdownOption: false,
      departmentID: this.itemID,
      departmentName: this.itemName,
    };
  },

  created() {},

  watch: {
    departmentID() {
      this.$emit("parseValue");
    },
  },

  methods: {
    /**
     * Hàm đóng mở drop down
     * NTDUONG
     */
    toggleDropdown() {
      try {
        this.isShowDropdownOption = !this.isShowDropdownOption;
        console.log(this.data);
        this.$refs.inputDropdown.focus();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm bắt sự kiện click item trong dropdown list
     * NTDUONG
     */
    hanldeItemClick(departmentID, departmentName) {
      try {
        this.departmentID = departmentID;
        this.departmentName = departmentName;
        this.toggleDropdown();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * set value dropdown
     * NTDUONG
     */
    setDropdownValue(id, name) {
      try {
        this.departmentID = id;
        this.departmentName = name;
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style lang="scss">
@import "../../css/base/dropdownlist.scss";
</style>
