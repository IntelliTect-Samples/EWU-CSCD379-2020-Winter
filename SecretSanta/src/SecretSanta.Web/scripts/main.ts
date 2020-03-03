import '../styles/site.scss';

import Vue from 'vue'

import GiftsComponent from './components/Gift/GiftsComponent.vue'
import UsersComponent from './components/User/UsersComponent.vue'
import GroupsComponent from './components/Group/GroupsComponent.vue'

document.addEventListener("DOMContentLoaded", async () => {
    if (document.getElementById('giftList')) {
        new Vue({
            render: l => l(GiftsComponent)
        }).$mount('#giftList');
    }

    if (document.getElementById('userList')) {
        new Vue({
            render: l => l(UsersComponent)
        }).$mount('#userList');
    }

    if (document.getElementById('groupList')) {
        new Vue({
            render: l => l(GroupsComponent)
        }).$mount('#groupList');
    }
});