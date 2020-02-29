﻿import '../styles/site.scss';

import { GiftList } from "./list-Gifts";


new GiftList().renderGifts();
 


function searchByTitle() {
    (new GiftList().searchGifts());
}


let btn = document.getElementById("searchButton");
btn.addEventListener("click", (e: Event) => searchByTitle());

