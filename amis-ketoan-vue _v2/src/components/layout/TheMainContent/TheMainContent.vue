<template>
  <EmployeePage></EmployeePage>
</template>
<script>
import EmployeePage from '@/views/employee/EmployeePage.vue';
export default {
  name: "TheMainContent",
  components: { EmployeePage },
};
</script>
<style lang="scss">
@import "../../../css/layout/page.scss";
</style>
