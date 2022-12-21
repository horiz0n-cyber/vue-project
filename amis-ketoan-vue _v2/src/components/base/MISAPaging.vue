<template>
  <!-- Vùng vẽ paging -->
  <div class="page-paging">
    <div class="page-paging-left">
      Tổng: <span class="total-record">{{ totalRecord }}</span>
    </div>
    <div class="page-paging-right">
      <div style="padding-right: 16px">Số bản ghi trên một trang</div>
      <div class="input-container dropdown m-t-0">
        <input
          v-model="recordsPerPage"
          ref="pageSettingInput"
          class="ms-input ms-input-icon dropdown-input page-setting"
          type="text"
          readonly
        />
        <div @click="togglePageSetting" class="icon btn-dropdown">
          <div class="ms-icon icon16 icon-dropdown"></div>
        </div>
        <div
          v-show="isShowPageSetting"
          class="dropdown-option-container page-setting-list"
        >
          <div class="dropdown-option">
            <div
              v-for="(option, index) in pageSettings"
              :key="index"
              class="dropdown-item"
              :class="{ selected: option == recordsPerPage }"
              @click="setRecordsPerPage(option)"
            >
              {{ option }}
            </div>
          </div>
        </div>
      </div>
      <div class="number-of-records">Trang
        <span class="from">{{ pageIndex }}</span> /
        <span class="to">{{ totalPage }}</span> 
      </div>
      <div class="icon icon24 m-r-8">
        <MISAIcon
          @click="onPrevButton"
          :class="{ disable: pageIndex == 1 || !totalPage }"
          size="icon14"
          icon="icon-chevron-left"
        ></MISAIcon>
      </div>
      <div class="icon icon24">
        <MISAIcon
          @click="onNextButton"
          :class="{ disable: pageIndex == totalPage || !totalPage }"
          size="icon14"
          icon="icon-chevron-right"
        ></MISAIcon>
      </div>
    </div>
  </div>
</template>
<script>
import MISAIcon from "./MISAIcon";
export default {
  name: "MISAPaging",
  components: { MISAIcon },
  props: ["totalRecord", "totalPage"],
  watch: {
    /**
     * Hàm thẽo dõi sự thay đổi của số bản ghi trên trang -> gọi emit lên lấy lại dữ liệu
     * NTDUONG
     * @param {giá trị số bản ghi / trang mới} newValue
     */
    recordsPerPage(newValue) {
      try {
        this.$emit("setRecordsPerPage", newValue);
      } catch (error) {
        console.log(error);
      }
    /**
     * Hàm theo dõi sự thay đổi của số trang -> gọi emit lên lấy lại dữ liệu
     */
    },
    pageIndex(newValue) {
      try {
        this.$emit("setPageIndex", newValue);
      } catch (error) {
        console.log(error);
      }
    },

  },
  data() {
    return {
      isShowPageSetting: false,
      pageSettings: [10, 20, 30, 50, 100],
      pageIndex: 1,
      recordsPerPage: 20,
      from: 0,
      to: 0,
    };
  },

  methods: {
    /**
     * Hàm đóng mở page setting
     * NTDUONG
     */
    togglePageSetting() {
      try {
        this.$refs.pageSettingInput.focus();
        this.isShowPageSetting = !this.isShowPageSetting;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xử lí click chọn option trong page setting
     * NTDUONG
     */
    setRecordsPerPage(option) {
      try {
        this.recordsPerPage = option;
        this.isShowPageSetting = false;
        this.$refs.pageSettingInput.focus();
        this.pageIndex = 1;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xử lí click nút next
     * NTDUONG
     */
    onNextButton() {
      try {
        if (this.pageIndex < this.totalPage) {
          this.pageIndex += 1;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xử lí click nút back
     * NTDUONG
     */
    onPrevButton() {
      try {
        if (this.pageIndex > 1) {
          this.pageIndex -= 1;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style lang="scss">
@import "../../css/layout/page.scss";
</style>
