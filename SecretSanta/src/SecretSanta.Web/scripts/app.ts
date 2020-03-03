import { GiftClient, Gift, UserClient, User } from './secretsanta-client';

export module App {
    export class Main {
        giftClient: GiftClient;
        userClient: UserClient;
        createdUser: User;

        constructor() {
            this.giftClient = new GiftClient('https://localhost:44388');
            this.userClient = new UserClient('https://localhost:44388');
        }
        async deleteGifts() {
            const gifts = await this.getGifts();

            await Promise.all(gifts.map(gift => this.giftClient.delete(gift.id)));
        }

        async createGifts() {
            const gifts = [1,2,3,4,5].map(i => {
                const gift = new Gift();
                gift.title = `Title ${i}`;
                gift.description = `Description ${i}`;
                gift.url = `Url ${i}`;
                gift.userId = this.createdUser.id;
                return this.giftClient.post(gift);
            });

            await Promise.all(gifts);
        }

        async getGifts(): Promise<Gift[]> {
            return await this.giftClient.getAll();
        }

        async createUser() {
            const users = await this.userClient.getAll();

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
