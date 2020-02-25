import { IGiftClient, GiftClient, Gift, User } from "./secretsanta-api.client"

export class App
{
    async renderGifts()
    {
        var gifts = await this.getAllGifts();

        const list = document.getElementById("giftsList");
        gifts.forEach(gift =>
        {
            const item = document.createElement("li");
            item.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`
            list.append(item);
        })
    }


    giftClient: IGiftClient;
    constructor(giftClient: IGiftClient = new GiftClient())
    {
        this.giftClient = giftClient;
    }

    async getAllGifts()
    {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }

    async generateGiftList()
    {
        await this.deleteAllGifts();

        let gifts: Gift[];
        for (var i = 0; i < 5; i++)
        {
            var gift = new Gift({ title: "Title", description: "Description", url: "http://www.google.com", userId: 1, id: i })
            this.giftClient.post(gift);
        }
    }

    async deleteAllGifts()
    {
        var gifts = await this.getAllGifts();

        for (var i = 0; i < gifts.length; i++)
        {
            await this.giftClient.delete(gifts[i].id);
        }
    }
}