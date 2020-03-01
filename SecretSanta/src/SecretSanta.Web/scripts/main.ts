import '../styles/site.scss';

import Vue from 'vue';

import Blah from './blah.vue';
import UsersComponent from './components/User/usersComponent.vue';

document.addEventListener("DOMContentLoaded", async () => {
    if (document.getElementById('UserPage')) {
        new Vue({
            render: h => h(UsersComponent)
        }).$mount('#UserPage');
    }
});