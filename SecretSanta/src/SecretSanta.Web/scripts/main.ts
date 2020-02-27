import '../styles/site.scss';

import { ListGifts } from "./List-Gifts";
import { UserClient, UserInput } from "./secret-santa-api.client"


const userId = async () => {
    var client = new UserClient();
    var users = await client.getAll();
    if (users.length != 0)
        return users[0].id;

    var userInput = new UserInput({
        firstName: "Ronald",
        lastName: "McDonald"
    });
    return (await client.post(userInput)).id;

};

const renderGifts = async () => {
    await userId().then(async value => {
        new ListGifts().renderGifts(value);
    }); 
}

renderGifts();