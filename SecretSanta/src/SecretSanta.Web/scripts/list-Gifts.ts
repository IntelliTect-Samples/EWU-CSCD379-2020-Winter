import {
    IGiftClient,
    IUserClient,
    GiftClient,
    UserClient,
    Gift,
    User
} from "./secretsanta-engine-api.client";


export class App
{
    async renderGifts()
    {
        var gifts = await this.getAllGifts();
        const itemList = document.getElementById("giftsList");
        gifts.forEach(gift => {
            
            const listItem = document.createElement("li");
            listItem.textContent = `${gift.id}:${gift.title}:${gift.description}:${gift.url}`;
            itemList.append(listItem);
        })
    }

    giftClient: IGiftClient;
    userClient: IUserClient;
    user: User;
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

    async createUser()
    {

        let userPost: User;

        userPost.firstName = "Jack";
        userPost.lastName = "File";
        await this.userClient.post(userPost);
    }

    async createGiftList()
    {

        let gifts: Gift[];

        for (var i = 0; i < 5; i++)
        {
            var gift = new Gift(
                {
                    title: "Title",
                    description: "Description",
                    url: "http://www.website.com",
                    userId: this.user.id,
                    id: i
                })
        }
    }
    
    async deleteAllGifts()
    {
        var gifts = await this.getAllGifts();
        gifts.forEach(async gift => {
            await this.giftClient.delete(gift.id);
        })
    }


}