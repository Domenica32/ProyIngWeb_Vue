import Vue from 'vue'
import App from './App.vue'
import router from './router/router'
import store from './store/store'
import vuetify from './plugins/vuetify'
import axios from 'axios'
import vSelect from 'vue-select'
import 'vue-select/dist/vue-select.css';

Vue.component('v-select' , vSelect)

Vue.config.productionTip = false
axios.defaults.baseURL='http://localhost:20497/'
new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
