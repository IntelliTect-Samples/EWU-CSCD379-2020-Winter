import { GiftClient, Gift, UserClient, User, GroupClient, Group } from './secretsanta-client';

export module App {
    export class Main {
        giftClient: GiftClient;
        userClient: UserClient;
        groupClient: GroupClient;
        createdUser: User;

        constructor() {
            this.giftClient = new GiftClient('https://localhost:44394');
            this.userClient = new UserClient('https://localhost:44394');
            this.groupClient = new GroupClient('https://localhost:44394');
        }
        async deleteGifts() {
            var gifts = await this.getGifts();

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

        async getGifts(): Promise<Gift[]> {
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