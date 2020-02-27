import {
    IGiftClient, GiftClient, Gift, User, IUserClient, UserClient
} from "./secretsanta-client";

export class GiftLister {
    client: IGiftClient;

    constructor(client: IGiftClient = new GiftClient) {
        this.client = client;
    }

    async deleteAllGifts() {
        var gifts = await this.client.getAll();
        for (let index = 0; index < gifts.length; index++) {
            await this.client.delete(gifts[index].id);
        }
    }

    async getUser() {
        var user = new User({
            firstName: "Inigo",
            lastName: "Montoya",
            santaId: null,
            gifts: null,
            groups: null,
            id: 1});

        var userClient = new UserClient();
        var users = await userClient.getAll();

        if (users.length > 0) {
            user = users[0];
        }
        else {
            user = await userClient.post(user);
        }

        return user;
    }

    async addGifts() {
        var num = Math.floor(Math.random() * (10 - 5 + 1)) + 5;

        var user = await this.getUser();

        for (let index = 0; index < num; index++) {
            var gift = new Gift({
                title: "gift title",
                description: "gift description",
                url: "http://www.google.com",
                userId: user.id,
                id: index
            });

            this.client.post(gift);
        }
    }

    async renderGifts() {
        await this.deleteAllGifts();
        await this.addGifts();
        var gifts = await this.getAllGifts();
        const itemList = document.getElementById("GiftLister");

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