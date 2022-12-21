<template>
  <div class="wrapper">
    <TheSidebar />
    <div class="main">
      <TheHeader />
      <TheMainContent
        ref="mainContent"
        @onGetData="onShowLoading"
        @afterGetData="offShowLoading"
      ></TheMainContent>
    </div>
  </div>
  <MISALoading v-show="isShowLoading" />
  
</template>

<script>
import TheHeader from "./components/layout/TheHeader/TheHeader.vue";
import TheMainContent from "./components/layout/TheMainContent/TheMainContent.vue";
import TheSidebar from "./components/layout/TheSidebar/TheSidebar.vue";
import MISALoading from "./components/base/MISALoading";

export default {
  name: "App",
  components: {
    TheSidebar,
    TheHeader,
    TheMainContent,
    MISALoading
  },
  data() {
    return {
      isShowLoading: false,
    };
  },
  methods: {
    onShowLoading() {
      try {
        this.isShowLoading = true;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm đóng loading
     * NTDUONG
     */
    offShowLoading() {
      this.isShowLoading = false;
    },
  },
};
</script>

<style lang="scss">
@import "./css/main.scss";
</style>
