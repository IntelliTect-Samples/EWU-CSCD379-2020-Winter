﻿import { IGiftClient, GiftClient,Gift,User } from "./secret-santa-api.client"

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

    async loadPage() {
        await this.deleteAllGifts();
        var Spongebob = new User({
            firstName: "Spongebob",
            lastName: "Squarepants",
            id: 1,
            gifts:  null,
            groups:  null,
            santaId: null
        });

        for (var i = 1; i < 6; i++) {
            var gift = new Gift({
                title: "LeSpatula",
                description: "LeSpatula 3000",
                id: i,
                url: "https://spongebob.fandom.com/wiki/Le_Spatula",
                userId: Spongebob.id
            });
            await this.giftClient.post(gift);
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

    async deleteAllGifts() {
        var gifts = await this.getAllGifts();
        for (var i = 0; i < gifts.length; i++){
            await this.giftClient.delete(gifts[i].id);
        }
    }
}
