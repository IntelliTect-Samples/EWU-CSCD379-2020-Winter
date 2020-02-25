
import './secretsanta-client.ts'
import { GiftClient, GiftInput, UserClient, UserInput, User } from "./secretsanta-client"

export class App {
    async getAllGifts() {
        var client = new GiftClient();
        var gifts = await client.getAll();
        return gifts;
    }

    async getGift(id) {
        var client = new GiftClient();
        return await client.get(id);
    }

    async getUser(id) {
        var client = new UserClient();
        return await client.get(id);
    }

    async deleteAllGifts() {
        var client = new GiftClient();
        var gifts = await client.getAll();
        for (var i in gifts) {
            await client.delete(gifts[i].id);
        }
    }

    async deleteAllUsers() {
        var client = new UserClient();
        var users = await client.getAll();
        for (var i in users) {
            await client.delete(users[i].id);
        }
    }

    async createGiftsCylonDetectors(userId, n) {
        var client = new GiftClient();
        var gift = new GiftInput();
        gift.title = "Cylon Detector";
        gift.description = "Version 1.0";
        gift.url = "www.find-a-cylon.com";
        gift.userId = userId;

        for (var i = 0; i < n; i++) {
            await client.post(gift);
        }
        
    }

    async createGiftCylonDetector(userId) {
        var client = new GiftClient();
        var gift = new GiftInput();
        gift.title = "Cylon Detector";
        gift.description = "Version 1.0";
        gift.url = "www.find-a-cylon.com";
        gift.userId = userId;
        return await client.post(gift);
    }
    async createGiftViper(userId) {
        var client = new GiftClient();
        var gift = new GiftInput();
        gift.title = "Viper";
        gift.description = "Fast Spaceship";
        gift.url = "www.vipers.com";
        gift.userId = userId;
        return await client.post(gift);
    }

    async createUserKaraThrace() {
        var client = new UserClient();
        var user = new UserInput();
        user.firstName = "Kara";
        user.lastName = "Thrace";
        return await client.post(user);
    }

    async createUserGaiusBaltar() {
        var client = new UserClient();
        var user = new UserInput();
        user.firstName = "Gaius";
        user.lastName = "Baltar";
        return await client.post(user);
    }
}

var app = new App();

var listId = "list-Gifts";
if (document.getElementById(listId) != null) {
    var loadingItem = document.createElement("li");
    loadingItem.textContent = "Loading...";
    document.getElementById(listId).appendChild(loadingItem);
}

app.deleteAllGifts().then(function () {

    app.deleteAllUsers();

}).then(function () {

    var user = null;
    app.createUserGaiusBaltar().then(function (value) {
        console.log("created user: ", value);
        user = value;
        app.createGiftsCylonDetectors(value.id, 5).then(function () {
            app.getAllGifts().then(function (value) {
                
                console.log("gifts: ", value);
                if (document.getElementById(listId) != null) {
                    document.getElementById(listId).removeChild(loadingItem);
                }
                for (var j in value) {

                    if (document.getElementById(listId) != null) {
                        var listItem = document.createElement("li");
                        listItem.textContent = value[j].title + " " + value[j].description + " " + value[j].url + " user: " + user.firstName + " " + user.lastName;
                        document.getElementById(listId).appendChild(listItem);
                    }
                    
                }
            });
        })
    });

});



