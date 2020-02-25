import {
    IGiftClient,
    GiftClient,
    Gift,
    User
} from "./secretsanta-client";

export class App {

    async renderGifts() {
        document.write("Hello World");
        var gifts = await this.retrieveGifts();
        const itemList = document.getElementById("giftList");
        gifts.forEach(gift => {
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`
            itemList.append(listItem);
        })
    }

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

    async createGiftList() {
        await this.deleteGifts();

        var user = new User({
            firstName: "Billy",
            lastName: "Bob",
            santaId: null,
            gifts: null,
            groups: null,
            id: 1
        });

        let gifts: Gift[];
        for (var i = 0; i < 5; i++) {
            var gift = new Gift({
                title: "Cool Gift",
                description: "Description",
                url: "http://www.cool.com",
                userId: 1,
                id: i
            })
            this.giftClient.post(gift);
        }
    }

    async deleteGifts() {
        var gifts = await this.retrieveGifts();
        for (var i = 0; i < gifts.length; i++) {
            await this.giftClient.delete(gifts[i].id);
        }
    }

    async retrieveGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }
}