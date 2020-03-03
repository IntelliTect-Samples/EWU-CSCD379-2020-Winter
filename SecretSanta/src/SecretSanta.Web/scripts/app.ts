import {GiftClient, Gift, UserClient, User, GroupClient} from './secretsanta-client';

export module App {
    export class Main {
        giftClient: GiftClient;
        userClient: UserClient;
        groupClient: GroupClient;
        createdUser: User;

        constructor() {
            this.giftClient = new GiftClient('http://localhost:5002');
            this.userClient = new UserClient('http://localhost:5002');
            this.groupClient = new GroupClient('http://localhost:5002');
        }
        async deleteGifts() {
            let gifts = await this.getGifts();

            for (let gift of gifts) {
                await this.giftClient.delete(gift.id);
            }
        }

        async createGifts() {
            for (let i = 0; i < 5; i++) {
                let gift = new Gift();
                gift.title = `Title ${i}`;
                gift.description = `Description ${i}`;
                gift.url = `Url ${i}`;
                gift.userId = this.createdUser.id;

                await this.giftClient.post(gift);
            }
        }

        async getGifts() {
            let gifts = await this.giftClient.getAll();

            return gifts;
        }

        async createUser() {
            let users = await this.userClient.getAll();

            if (users.length > 0) {
                this.createdUser = users[0];
            }
            else {
                this.createdUser = new User();
                this.createdUser.firstName = 'Inigo';
                this.createdUser.lastName = 'Montoya';
                await this.userClient.post(this.createdUser);
            }
        }
    }
}