import { App } from "./page";
import { Gift, IGift } from "./secretsanta-client";

const app = new App();

export function renderGifts() {
    app.deleteAllGifts();
    app.addAllGifts(giftsArray());
    generateGifts(app);
}


function generateGifts(app: App) {
    app.getAllGifts().then((giftsArray: Gift[]) => {
        giftsArray.forEach((gift) => {
            let item: HTMLLIElement = document.createElement("li");
            item.textContent = convert(gift);
            document.getElementById("giftsPage").append(item);
        });
    });
}

function convert(gift: Gift): string {
    return `Title: ${gift.title}    Description: ${gift.description}    Url: ${gift.url}`;
}

function giftsArray(): Gift[] {
    var giftsArray: Gift[] = [];
    giftsArray[0] = new Gift(createGift(1, "Title", "Description", "Url"));
    giftsArray[1] = new Gift(createGift(2, "box", "boring", "www.amazon.com"));
    giftsArray[2] = new Gift(createGift(3, "bike", "fun and quick", "www.bikestore.com"));
    giftsArray[3] = new Gift(createGift(4, "socks", "warm", "www.sockstore.com"));
    giftsArray[4] = new Gift(createGift(5, "shoe", "flashy", "www.shoes.com"));
    giftsArray[5] = new Gift(createGift(6, "TV", "expensive", "www.bestbuy.com"));
    giftsArray[6] = new Gift(createGift(7, "computer", "more expensive", "www.amazon.com"));
    return giftsArray;
}

function createGift(id: number, title: string, description: string, url: string): IGift {
    let toReturn: IGift = new Gift();
    toReturn.title = title;
    toReturn.description = description;
    toReturn.url = url;
    toReturn.userId = 1;
    return toReturn;
}