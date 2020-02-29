import { IGiftClient, GiftClient, Gift, User, IUserClient, UserClient, UserInput } from "./secretsanta-api.client"

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
    userClient: IUserClient;
    constructor(giftClient: IGiftClient = new GiftClient(), userClient: IUserClient = new UserClient())
    {
        this.giftClient = giftClient;
        this.userClient = userClient;
    }

    async getAllGifts()
    {
        var gifts = await this.giftClient.getAll();
        return gifts;
    }

    async generateUser()
    {
        let userPosted: User;
        userPosted.firstName = "Inigo";
        userPosted.lastName = "Montoya";
        await this.userClient.post(userPosted);
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