import { Gift, GiftClient, IGiftClient } from "./secretsanta-client";

export class App {

    async renderGifts() {
        var gifts = await this.getGifts();

        const list = document.getElementById("giftList");
        gifts.forEach(gift => {
            const item = document.createElement("li");
            item.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`
            list.append(item);
        })
    }

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

    async getGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }

    async deleteGifts() {
        var gifts = await this.getGifts();

        for (var ix = 0; ix < gifts.length; ix++) {
            await this.giftClient.delete(gifts[ix].id);
        }
    }

    async generateGifts() {
        await this.deleteGifts();

        let gifts: Gift[];
        for (var ix = 0; ix < 10; ix++) {
            var gift = new Gift({ title: "Title", description: "Description", url: "www.ewu.edu", userId: 1, id: ix })
            this.giftClient.post(gift);
        }
    }
}