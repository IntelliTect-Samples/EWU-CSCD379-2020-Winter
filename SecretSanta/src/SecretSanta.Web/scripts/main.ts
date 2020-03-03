import '../styles/site.scss';

import Vue from 'vue';

import Gifts from './components/Gifts/Gifts.vue';
import Users from './components/Users/Users.vue';
import Groups from './components/Groups/Groups.vue';

document.addEventListener("DOMContentLoaded", async () => {
    if (document.getElementById('giftList')) {
        new Vue({
            render: h => h(Gifts)
        }).$mount('#giftList');
    }
    if (document.getElementById('userList')) {
        new Vue({
            render: h => h(Users)
        }).$mount('#userList');
    }
    if (document.getElementById('groupList')) {
        new Vue({
            render: h => h(Groups)
        }).$mount('#groupList');
    }
});
