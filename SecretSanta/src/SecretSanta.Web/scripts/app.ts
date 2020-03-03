import { GiftClient, Gift, UserClient, User, GroupClient, Group } from './secretsanta-client';

export module App {
    export class Main {
        giftClient: GiftClient;
        userClient: UserClient;
        groupClient: GroupClient;
        createdUser: User;

        constructor() {
            this.giftClient = new GiftClient('https://localhost:44388');
            this.userClient = new UserClient('https://localhost:44388');
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
            var gifts = await this.giftClient.getAll();

            return gifts;
        }

        async getGroups(): Promise<Group[]> {
            var groups = await this.groupClient.getAll();

            return groups;
        }

        async getUsers(): Promise<User[]> {
            var users = await this.userClient.getAll();

            return users;
        }


        async createUser() {
            var users = await this.userClient.getAll();

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

        async deleteUsers() {
            var users = await this.getUsers();

            for (let user of users) {
                await this.userClient.delete(user.id);
            }
        }

        async deleteGroups() {
            var groups = await this.getGroups();

            for (let group of groups) {
                await this.groupClient.delete(group.id);
            }
        }
    }
}