import {
    IGiftClient,
    GiftClient,
    Gift,
    User,
    UserClient,
    IUserClient
} from "./secretsanta-client"

export class App {
    createdUser: User;
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
    userClient: IUserClient;
    constructor(giftClient: IGiftClient = new GiftClient(), userClient: IUserClient = new UserClient()) {
        this.giftClient = giftClient;
        this.userClient = userClient;
    }

    async createGiftList() {
        await this.deleteGifts();
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

    async createUser() {
        this.createdUser = new User();
        this.createdUser.firstName = "Inigo";
        this.createdUser.lastName = "Montoya";
        await this.userClient.post(this.createdUser);
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