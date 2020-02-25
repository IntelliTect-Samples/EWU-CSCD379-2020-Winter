import {
    IGiftClient, GiftClient, Gift, User
} from "./secretsanta-client";

export class GiftLister {
    client: IGiftClient;

    constructor(client: IGiftClient = new GiftClient) {
        this.client = client;
    }

    async renderGifts() {
        var gifts = await this.getAllGifts();
        const itemList = document.getElementById("GiftLister");
        //document.write("Hello World");

        for (let index = 0; index < gifts.length; index++) {
            const gift = gifts[index];
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`;
            itemList.append(listItem);
        }
    }

    async getAllGifts() {
        var gifts = await this.client.getAll();
        return gifts;
    }
}