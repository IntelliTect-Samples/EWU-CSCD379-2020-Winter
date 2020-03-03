import '../styles/site.scss';

import Vue from 'vue';

import GiftsComponent from './components/Gift/giftsComponent.vue';
import GroupsComponent from './components/Group/groupsComponent.vue';
import UsersComponent from './components/User/usersComponent.vue';
import { App } from './app';

//import { Gift } from './secretsanta-client';

document.addEventListener("DOMContentLoaded", async () => {

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

    if (document.getElementById('userList')) {
        new Vue({
            render: h => h(UsersComponent)
        }).$mount('#userList');
    }
});





document.addEventListener("DOMContentLoaded", async () => {

    let app = new App.Main();

    //await app.deleteGifts();
    //await app.createUser();
    //await app.createGifts();

    //let gifts = await app.getGifts();
    //let element = document.getElementById('giftList');
    //for (let gift of gifts) {
    //    let liElement = element.appendChild(document.createElement('li'));
    //    liElement.textContent = `${gift.id} ${gift.title} ${gift.description} ${gift.url}`;
    //}
    
});