import '../styles/site.scss';

import Vue from 'vue';

import Blah from './blah.vue';
import UsersComponent from './components/User/usersComponent.vue';
import GroupsComponent from './components/Group/groupsComponent.vue';
import GiftsComponent from './components/Gift/giftsComponent.vue';

document.addEventListener("DOMContentLoaded", async () => {
    if (document.getElementById('UserPage')) {
        new Vue({
            render: h => h(UsersComponent)
        }).$mount('#UserPage');
    }
    else if (document.getElementById('GroupPage')) {
        new Vue({
            render: h => h(GroupsComponent)
        }).$mount('#GroupPage');
    }
    else if (document.getElementById('GiftPage')) {
        new Vue({
            render: h => h(GiftsComponent)
        }).$mount('#GiftPage');
    }
});