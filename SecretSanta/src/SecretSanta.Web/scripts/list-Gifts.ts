import { IGiftClient, GiftClient } from "./secret-santa-api.client"

export class App
{
    async renderGifts() {
        var gifts = await this.getAllGifts();
        const giftsPage = document.getElementById("giftsPage");
        for (let index = 0; index < gifts.length; index++) {
            const gift = gifts[index];
            const listItem = document.createElement("li");
            listItem.textContent = '${gift.title}: ${gift.description}: ${gift.url}';
            giftsPage.append(listItem);
        } 
    }

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient; 
    }

    async getAllGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }
}
