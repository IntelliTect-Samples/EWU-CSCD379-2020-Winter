import '../styles/site.scss';

import Vue from 'vue';

import Gifts from './components/Gifts/Gifts.vue';

document.addEventListener("DOMContentLoaded", async () => {
    if (document.getElementById('giftList')) {
        new Vue({
            render: h => h(Gifts)
        }).$mount('#giftList');
    }
});
