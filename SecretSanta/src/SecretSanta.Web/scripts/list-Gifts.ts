import { IGiftClient, GiftClient, Gift, IUserClient, UserClient, User } from "./secret-santa-api.client"

export class App
{
    async renderGifts() {
        var gifts = await this.getAllGifts();
        const giftsPage = document.getElementById("giftsPage");
        for (let index = 0; index < gifts.length; index++) {
            const gift = gifts[index];
            var title = "Title: " + gift.title;
            var desc = "Description: " + gift.description;
            var url = "URL: " + gift.url;
            const listItem = document.createElement("li");
            listItem.textContent = title + ",   " + desc + ",   " + url;
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
        await this.userClient.post(Spongebob);


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
    userClient: IUserClient;
    constructor(giftClient: IGiftClient = new GiftClient(), userClient: IUserClient = new UserClient()) {
        this.giftClient = giftClient;
        this.userClient = userClient;
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
