import '../styles/site.scss';
import Vue from 'vue'

import Users from './components/User/usersComponent.vue';
import Gifts from './components/Gift/giftsComponent.vue';
import Groups from './components/Group/groupsComponent.vue';

document.addEventListener("DOMContentLoaded", async () => {
   if (document.getElementById('users')) {
      new Vue({
         render: h => h(Users)
      }).$mount('#users');
   }
   
   if (document.getElementById('gifts')) {
      new Vue({
         render: h => h(Gifts)
      }).$mount('#gifts');
   }

   if (document.getElementById('groups')) {
      new Vue({
         render: h => h(Groups)
      }).$mount('#groups');
   }
});