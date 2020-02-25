import '../styles/site.scss';

import { App } from './listGifts'

let app = new App();

app.generateGiftList();
app.renderGifts();