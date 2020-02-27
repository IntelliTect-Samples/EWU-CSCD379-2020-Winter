import {
    IGiftClient,
    GiftClient,
    User,
    Gift
} from "./secretsanta-engine.client";


export class App {

    async renderGifts() {
      //  document.write("Hello World");
     //   console.log("hello world")
        var gifts = await this.retrieveGifts();
        const giftList = document.getElementById("giftList");
        gifts.forEach(gift => {
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`
            giftList.append(listItem);
        })
    }

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

    async createGiftList() {
        await this.deleteGifts();
        await this.createUser();

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

    async createUser() {
        var user = new User({
            firstName: "Billy",
            lastName: "Bob",
            santaId: 1,
            gifts: null,
            groups: null,
            id: 1
        });
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