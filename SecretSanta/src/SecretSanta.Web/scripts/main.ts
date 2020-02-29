import '../styles/site.scss';

import './list-Gifts.ts'
import { App } from './list-Gifts';
import { GiftClient } from './secretsanta-client';

var app = new App(new GiftClient());
app.render();