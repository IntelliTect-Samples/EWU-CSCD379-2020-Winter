"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var page_1 = require("./page");
var secretsanta_client_1 = require("./secretsanta-client");
var app = new page_1.App();
function renderGifts() {
    app.deleteAllGifts();
    app.addAllGifts(giftsArray());
    generateGifts(app);
}
exports.renderGifts = renderGifts;
function generateGifts(app) {
    app.getAllGifts().then(function (giftsArray) {
        giftsArray.forEach(function (gift) {
            var item = document.createElement("li");
            item.textContent = convert(gift);
            document.getElementById("giftsPage").append(item);
        });
    });
}
function convert(gift) {
    return "Title: " + gift.title + "    Description: " + gift.description + "    Url: " + gift.url;
}
function giftsArray() {
    var giftsArray = [];
    giftsArray[0] = new secretsanta_client_1.Gift(createGift(1, "Title", "Description", "Url"));
    giftsArray[1] = new secretsanta_client_1.Gift(createGift(2, "box", "boring", "www.amazon.com"));
    giftsArray[2] = new secretsanta_client_1.Gift(createGift(3, "bike", "fun and quick", "www.bikestore.com"));
    giftsArray[3] = new secretsanta_client_1.Gift(createGift(4, "socks", "warm", "www.sockstore.com"));
    giftsArray[4] = new secretsanta_client_1.Gift(createGift(5, "shoe", "flashy", "www.shoes.com"));
    giftsArray[5] = new secretsanta_client_1.Gift(createGift(6, "TV", "expensive", "www.bestbuy.com"));
    giftsArray[6] = new secretsanta_client_1.Gift(createGift(7, "computer", "more expensive", "www.amazon.com"));
    return giftsArray;
}
function createGift(id, title, description, url) {
    var toReturn = new secretsanta_client_1.Gift();
    toReturn.title = title;
    toReturn.description = description;
    toReturn.url = url;
    toReturn.userId = 1;
    return toReturn;
}
//# sourceMappingURL=list-Gifts.js.map