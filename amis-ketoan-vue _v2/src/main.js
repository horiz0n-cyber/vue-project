import { createApp } from 'vue'
import App from './App.vue'

// axios
import axios from 'axios'
import VueAxios from 'vue-axios'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

// click outside element
import vueClickOutsideElement from 'vue-click-outside-element'

// Vue DatePicker
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/src/VueDatePicker/style/main.scss';

// Vue router
import EmployeePage from "./views/employee/EmployeePage.vue"
import CustomerPage from "./views/customer/CustomerPage.vue"
import HomePage from "./views/HomePage.vue"
// B1: Import
import {createRouter, createWebHistory} from 'vue-router'
// B2 Khai báo router
const routers = [
  {
    path: "/employees",
    name: "EmployeePage",
    component: EmployeePage
  },
  {
    path: "/customers",
    name: "CustomerPage",
    component: CustomerPage
  },
  {
    path: "/",
    name: "HomePage",
    component: HomePage
  }
]
// B3 Khởi tạo router
const router = createRouter({
  history: createWebHistory(),
  routes: routers
})

const vuetify = createVuetify({
  components,
  directives,
})
createApp(App).use(vuetify).use(VueAxios, axios).use(router).component('Datepicker', Datepicker).use(vueClickOutsideElement).mount('#app')
