import '../styles/site.scss';
import { App } from "./list-Gifts";

let app = new App();

app.generateGiftList();
app.renderGifts();