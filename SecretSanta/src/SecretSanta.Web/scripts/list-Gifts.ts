import { IGiftClient, GiftClient, Gift, User } from "./secretsanta-client"

export const hello = () => 'Hello world!';


export class App {
    async renderGifts() {
        await this.generateGifts();
        var gifts = await this.getAllGifts();
        const giftList = document.getElementById("giftList");
        for (let index = 0; index < gifts.length; index++) {
            const gift = gifts[index];
            //document.write("Hello World");
            const listGift = document.createElement("li");
            listGift.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`;
            giftList.append(listGift);
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

    async generateGifts() {
        await this.deleteGifts();

        for (var i = 0; i < 5; i++) {
            var gift = new Gift({
                title: "Title",
                description: "Description",
                url: "www.Test.com",
                userId: 1,
                id: i
            });

            await this.giftClient.post(gift);
        }
    }

    async deleteGifts() {
        var gifts = await this.getAllGifts();

        for (var i = 0; i < gifts.length; i++) {
            await this.giftClient.delete(gifts[i].id);
        }
    }
}