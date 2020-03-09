import '../styles/site.scss';
import Vue from 'vue';

import UsersComponent from './components/User/usersComponent.vue';
import GiftsComponent from './components/Gift/giftsComponent.vue';
import GroupsComponent from './components/Group/groupsComponent.vue';

document.addEventListener("DOMContentLoaded", async () => {
    if (document.getElementById('userList')) {
        new Vue({
            render: h => h(UsersComponent)
        }).$mount('#userList');
    }
    if (document.getElementById('giftList')) {
        new Vue({
            render: h => h(GiftsComponent)
        }).$mount('#giftList');
    }
    if (document.getElementById('groupList')) {
        new Vue({
            render: h => h(GroupsComponent)
        }).$mount('#groupList');
    }
});

import { App } from './app';

document.addEventListener("DOMContentLoaded", async () => {

    let app = new App.Main();
    if (app.createdUser == null) {
        await app.createUser();
    }
    

});