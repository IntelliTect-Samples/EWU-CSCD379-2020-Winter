import {
    GiftClient,
    UserClient,
    GroupClient,
    Gift
} from "./secretsanta-client"

export class App {
    async getAllUsers() {
        var userClient = new UserClient();
        var users = await userClient.getAll();
        return users;
    }
    async getAllGroups() {
        var groupClient = new GroupClient();
        var groups = await groupClient.getAll();
        return groups;
    }
    async getAllGifts() {
        var giftClient = new GiftClient();
        var gifts = await giftClient.getAll();
        return gifts;
    }
    async addAllGifts(giftArray: Gift[]) {
        var giftClient = new GiftClient();
        giftArray.forEach((gift) => {
            giftClient.post(gift);
        });
    }
    async deleteAllGifts() {
        var giftClient = new GiftClient();
        getAllGifts().then((giftArray) => {
            giftArray.forEach((gift) => {
                giftClient.delete(gift.id);
            });
        });
    }
}


export async function getAllUsers() {
    var userClient = new UserClient();
    var users = await userClient.getAll();
    return users;
}

export async function getAllGroups() {
    var groupClient = new GroupClient();
    var groups = await groupClient.getAll();
    return groups;
}

export async function getAllGifts() {
    var giftClient = new GiftClient();
    var gifts = await giftClient.getAll();
    return gifts;
}