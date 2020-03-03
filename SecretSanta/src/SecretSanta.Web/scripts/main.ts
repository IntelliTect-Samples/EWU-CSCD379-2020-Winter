import '../styles/site.scss';
import { App } from './app';
import GiftsComponent from './Components/Gift/giftComponent.vue';
import GroupsComponent from './Components/Group/groupComponent.vue';
import UsersComponent from './Components/User/userComponent.vue';
import Vue from 'vue';


if (document.getElementById('giftList')) {
    new Vue({
        render: h => h(GiftsComponent)
    }).$mount('#giftList');
}

if (document.getElementById('userList')) {
    new Vue({
        render: h => h(UsersComponent)
    }).$mount('#userList');
}

if (document.getElementById('groupList')) {
    new Vue({
        render: h => h(GroupsComponent)
    }).$mount('#groupList');
}

document.addEventListener("DOMContentLoaded", async () => {
    let app = new App.Main();

});