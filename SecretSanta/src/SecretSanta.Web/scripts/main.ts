import '../styles/site.scss';
import Vue from 'vue';
import UsersComponent from './components/User/usersComponent.vue';
import GroupsComponent from './components/Group/groupsComponent.vue';
import GiftsComponent from './components/Gift/giftsComponent.vue';

document.addEventListener("DOMContentLoaded", async () => {
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

    if (document.getElementById('giftList')) {
        new Vue({
            render: h => h(GiftsComponent)
        }).$mount('#giftList');
    }
});
