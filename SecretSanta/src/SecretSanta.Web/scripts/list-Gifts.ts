﻿import {Gift, GiftClient, IGiftClient} from "./secretsanta-client";

export class App {

    async displayGifts() {
        let gifts = await this.getAllGifts();
        let giftList = document.getElementById("giftList");
        gifts.forEach(gift => {
            let item = document.createElement("li");
            item.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`;
            giftList.append(item);
        });
    }

    giftClient: IGiftClient;

    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

    async createGifts() {
        await this.deleteGifts();
        let gifts: Gift[];
        for (let i = 0; i < 5; i++) {
            let gift = new Gift({
                title: "Example",
                description: "Description",
                url: "http://www.example.com",
                userId: 1,
                id: i
            });
            await this.giftClient.post(gift);
        }
    }

    async getAllGifts() {
        return await this.giftClient.getAll();
    }

    async deleteGifts() {
        let gifts = await this.getAllGifts();

        for (let i = 0; i < gifts.length; i++) {
            await this.giftClient.delete(gifts[i].id);
        }
    }
}
