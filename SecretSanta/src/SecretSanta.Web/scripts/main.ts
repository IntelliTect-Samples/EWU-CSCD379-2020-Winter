﻿import '../styles/site.scss';
import { App } from "./list-Gifts";

let app = new App();

app.deleteAllGifts();
app.createUser();
app.generateGiftList();
app.renderGifts();
