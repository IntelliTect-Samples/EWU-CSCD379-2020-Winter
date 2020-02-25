
import './secretsanta-client.ts'
import { GiftClient, GiftInput, UserClient, UserInput } from "./secretsanta-client"

export class App {
    async getAllGifts() {
        var client = new GiftClient();
        var gifts = await client.getAll();
        console.log("gifts: ", gifts);
        return gifts;
    }

    async createGift() {
        var client = new GiftClient();
        var gift = new GiftInput();
        gift.title = "title";
        gift.description = "description";
        gift.url = "url";
        var user = await this.createUser();
        gift.userId = 1;
        client.post(gift);
    }

    async createUser() {
        var client = new UserClient();
        var user = new UserInput();
        user.firstName = "kara";
        user.lastName = "thrace";
        client.post(user);
    }
}

var app = new App();
app.createGift();
app.getAllGifts();