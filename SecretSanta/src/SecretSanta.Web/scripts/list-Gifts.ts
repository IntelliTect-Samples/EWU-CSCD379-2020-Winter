import { IGiftClient, GiftClient, Gift, User, UserClient, IUserClient } from "./secretsanta-client"

export const hello = () => 'Hello world!';


export class App {
    async renderGifts() {
        await this.generateGifts();
        var gifts = await this.getAllGifts();
        const giftList = document.getElementById("giftList");
        for (let index = 0; index < gifts.length; index++) {
            const gift = gifts[index];
            //document.write("Hello World");
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`;
            giftList.append(listItem);
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

    async generateGifts() {
        await this.deleteGifts();
        await this.createUser();

        var userClient = new UserClient();
        var users = await userClient.getAll();

        for (var i = 0; i < 5; i++) {
            var gift = new Gift({
                title: "Title",
                description: "Description",
                url: "www.Test.com",
                userId: users[0].id,
                id: i
            });

            await this.giftClient.post(gift);
        }
    }

    async deleteGifts() {
        var gifts = await this.getAllGifts();

        for (var i = 0; i < gifts.length; i++) {
            await this.giftClient.delete(gifts[i].id);
        }
    }

    async createUser() {
        await this.deleteUsers();

        var user = new User({
            firstName: "Inigo",
            lastName: "Montoya",
            gifts: null,
            groups: null,
            id: 42
        });

        var userClient = new UserClient();
        await userClient.post(user);

    }

    async deleteUsers() {
        var userClient = new UserClient();
        var users = await userClient.getAll();

        for (var i = 0; i < users.length; i++) {
            await userClient.delete(users[i].id);
        }
    }
}