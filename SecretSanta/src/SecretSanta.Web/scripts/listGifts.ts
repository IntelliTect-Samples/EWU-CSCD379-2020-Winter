import {
    IGiftClient,
    GiftClient,
    Gift,
    User
} from "./secretsanta-client";

export class App {

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

}