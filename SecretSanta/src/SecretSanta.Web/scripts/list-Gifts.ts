
import './secretsanta-client.ts'
import { GiftClient, GiftInput, UserClient, UserInput, User, IGiftClient } from "./secretsanta-client"

export class App {

    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient()) {
        this.giftClient = giftClient;
    }

    async getAllGifts() {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }

    async searchGifts(searchTerm) {
        var gifts = await this.giftClient.search(searchTerm);
        //var gifts = await client.getAll();
        return gifts;
    }

    async getGift(id) {
        return await this.giftClient.get(id);
    }

    async getUser(id) {
        return await this.giftClient.get(id);
    }

    async deleteAllGifts() {
        var gifts = await this.giftClient.getAll();
        for (var i in gifts) {
            await this.giftClient.delete(gifts[i].id);
        }
    }

    async deleteAllUsers() {
        var users = await this.giftClient.getAll();
        for (var i in users) {
            await this.giftClient.delete(users[i].id);
        }
    }

    async createGiftsCylonDetectors(userId, n) {
        var gift = new GiftInput();
        gift.title = "Cylon Detector";
        gift.description = "Version 1.0";
        gift.url = "www.find-a-cylon.com";
        gift.userId = userId;

        for (var i = 0; i < n; i++) {
            await this.giftClient.post(gift);
        }

    }

    async createGiftCylonDetector(userId) {
        var gift = new GiftInput();
        gift.title = "Cylon Detector";
        gift.description = "Version 1.0";
        gift.url = "www.find-a-cylon.com";
        gift.userId = userId;
        return await this.giftClient.post(gift);
    }
    async createGiftViper(userId) {
        var gift = new GiftInput();
        gift.title = "Viper";
        gift.description = "Fast Spaceship";
        gift.url = "www.vipers.com";
        gift.userId = userId;
        return await this.giftClient.post(gift);
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

    async render() {

        var listId = "list-Gifts";
        let app = this;
        if (document.getElementById("giftSearchButton") != null) {
            document.getElementById("giftSearchButton").addEventListener("click", function (e) {
                var searchTerm = (<HTMLInputElement>document.getElementById("giftSearchText")).value;
                console.log("searchTerm: " + searchTerm);

                document.getElementById(listId).innerHTML = "";
                var loadingItem = document.createElement("li");
                loadingItem.textContent = "Loading...";
                document.getElementById(listId).appendChild(loadingItem);

                app.searchGifts(searchTerm).then(function (value) {

                    console.log("gifts: ", value);
                    if (document.getElementById(listId) != null) {
                        document.getElementById(listId).removeChild(loadingItem);
                    }


                    if (document.getElementById(listId) != null) {
                        var listItem = document.createElement("li");
                        listItem.textContent = value.title + " " + value.description + " " + value.url;
                        document.getElementById(listId).appendChild(listItem);
                    }



                }).catch(function () {
                    if (document.getElementById(listId) != null) {
                        document.getElementById(listId).removeChild(loadingItem);
                    }
                });
            });
        }
        



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

    }
}








