import '../styles/site.scss';

import { GiftLister } from "./list-Gifts";

let giftLister = new GiftLister();

giftLister.deleteAllGifts();
giftLister.renderGifts();
