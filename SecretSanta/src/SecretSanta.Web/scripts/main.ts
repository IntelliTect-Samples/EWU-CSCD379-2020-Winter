import '../styles/site.scss';
import { App } from './app';
import { Gift } from './secretsanta-client';
import Vue from 'vue';
import UsersComponent from './components/User/usersComponent.vue';
document.addEventListener("DOMContentLoaded", async () => {
    if (document.getElementById('userList')) {
        new Vue({
            render: h => h(UsersComponent)
        }).$mount('#userList');
    }
});
//document.addEventListener("DOMContentLoaded", async () => {
//    let app = new App.Main();

//    await app.deleteGifts();

//    await app.createUser();

//    await app.createGifts();

//    let gifts = await app.getGifts();

//    let element = document.getElementById('giftList');

//    for (let gift of gifts) {
//        let liElement = element.appendChild(document.createElement('li'));
//        liElement.textContent = `${gift.id} ${gift.title} ${gift.description} ${gift.url}`;
//    }
    
//});