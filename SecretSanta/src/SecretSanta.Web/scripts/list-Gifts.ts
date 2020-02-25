import {
    IGiftClient,
    GiftClient,
    Gift,
    User
} from "./secretsanta-client"

export class App {
    async displayGifts() {
        var gifts = await this.getAllGifts();
        const list = document.getElementById("giftList");
        gifts.forEach(g => {
            const item = document.createElement("li");
            item.textContent = `${g.id}:${g.title}:${g.description}:${g.url}`
            list.append(item);
        })
    }

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

    async createGiftList() {
        await this.deleteGifts();
        var newUser = new User({
            firstName: "Jerett",
            lastName: "Latimer",
            gifts: null,
            groups: null,
            santaId: null,
            id: 1
        });

        let gifts: Gift[];
        for (var i = 0; i < 5; i++) {
            var newGift = new Gift({
                title: "Ring",
                description: "Doorbell",
                url: "www.ring.com",
                userId: 1,
                id: i
            })
        }

        this.giftClient.post(newGift);
    }

    async getAllGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }

    async deleteGifts() {
        var gifts = await this.getAllGifts();
        for (var i = 0; i < gifts.length; i++) {
            await this.giftClient.delete(gifts[i].id);
        }
    }
}