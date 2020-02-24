import {
    IGiftClient,
    GiftClient,
    Gift,
    User
} from "./secretsanta-client";

export class App {
    async renderGifts() {
        var gifts = await this.retrieveAllGifts();
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

    async generateGiftList() {
        await this.deleteAllGifts();

        var user = new User({
            firstName: "Inigo",
            lastName: "Montoya",
            santaId: null,
            gifts: null,
            groups: null,
            id: 1
        });

        let gifts: Gift[];
        for (var i = 0; i < 5; i++) {
            var gift = new Gift({
                title: "A Title",
                description: "Some Description",
                url: "http://www.greatgift.com",
                userId: 1,
                id: i
            })

            this.giftClient.post(gift);
        }
    }

    async deleteAllGifts() {
        var gifts = await this.retrieveAllGifts();
        for (var i = 0; i < gifts.length; i++) {
            await this.giftClient.delete(gifts[i].id);
        }
    }

    async retrieveAllGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }
} 