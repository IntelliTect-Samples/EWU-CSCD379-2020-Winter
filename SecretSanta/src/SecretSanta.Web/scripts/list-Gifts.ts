import {
    IGiftClient, GiftClient, Gift, User
} from "./secretsanta-client";

export class GiftLister {
    client: IGiftClient;

    constructor(client: IGiftClient = new GiftClient) {
        this.client = client;
    }

    async deleteAllGifts() {
        var gifts = await this.client.getAll();
        for (let index = 0; index < gifts.length; index++) {
            const gift = gifts[index];
            this.client.delete(gift.id);
        }
    }

    async addGifts() {
        var num = Math.floor(Math.random() * (20 - 10 + 1)) + 10;

        var user = new User({
            firstName: "Inigo",
            lastName: "Montoya",
            santaId: null,
            gifts: null,
            groups: null,
            id: 1});

        for (let index = 0; index < num; index++) {
            var gift = new Gift({
                title: "gift title",
                description: "gift description",
                url: "url!",
                userId: 1,
                id: index
            });

            this.client.post(gift);
        }
    }

    async renderGifts() {
        var gifts = await this.getAllGifts();
        const itemList = document.getElementById("GiftLister");
        document.write("Hello World");

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