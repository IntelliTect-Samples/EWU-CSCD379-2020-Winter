import {
    GiftClient,
    IGiftClient,
    GiftInput
} from "./secret-santa-api.client";

export const hello = () => 'Hello world!';

export class ListGifts {
    

    giftClient: IGiftClient;
    userId : number;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
        this.userId = 0;
    }

    async renderGifts(userId: number) {
        this.userId = userId;
        var gifts = await this.createNewGiftList()
            .then(async () => await this.getAllGifts());
        const giftsPage = document.getElementById("giftsPage");
        for(let index = 0; index < gifts.length; index ++){
            const gift = gifts[index];
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.title}:${gift.description}`;
            giftsPage.append(listItem);
        }
    };

    async getAllGifts() {
        var gift = await this.giftClient.getAll();
        return gift;
    };

    async createNewGiftList() {
        await this.deleteAllGifts().then(async () => {
            await this.createGifts();
        });
        
    }

    async deleteAllGifts() {
        var gifts = await this.getAllGifts();
        for (let gift of gifts) {
            await this.giftClient.delete(gift.id);
        }
        return;
    }

    async createGifts() {

        for (let index = 0; index < 6; index++) {
            var giftInput = new GiftInput({
                title: "Cheese Burger",
                description: "A Cheese Burger",
                url: "http://www.Cheesebur.gr",
                userId: this.userId
            });
            await this.giftClient.post(giftInput);
        }
        
    }


}