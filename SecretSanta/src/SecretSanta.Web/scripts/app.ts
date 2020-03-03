import { GiftClient, Gift, UserClient, User, GroupClient, GiftInput, UserInput, GroupInput, Group } from './secretsanta-client';

export module App {
    export class Main {
        giftClient: GiftClient;
        userClient: UserClient;
        groupClient: GroupClient;
        createdUser: User;

        constructor() {
            this.giftClient = new GiftClient('https://localhost:44388');
            this.userClient = new UserClient('https://localhost:44388');
            this.groupClient = new GroupClient('https://localhost:44388');
        }

        async insertGift(giftInput: GiftInput): Promise<Gift> {
            var gift = await this.giftClient.post(giftInput);
            return gift;
        }
        async getGifts(): Promise<Gift[]> {
            var gifts = await this.giftClient.getAll();
            return gifts;
        }
        async getGift(id: number): Promise<Gift> {
            var gift = await this.giftClient.get(id);
            return gift;
        }
        async updateGift(gift: Gift): Promise<Gift> {
            var gift = await this.giftClient.put(gift.id, gift);
            return gift;
        }
        async deleteGift(id: number): Promise<void> {
            await this.giftClient.delete(id);
        }

        async insertUser(userInput: UserInput): Promise<User> {
            var user = await this.userClient.post(userInput);
            return user;
        }
        async getUsers(): Promise<User[]> {
            var users = await this.userClient.getAll();
            return users;
        }
        async getUser(id: number): Promise<User> {
            var user = await this.userClient.get(id);
            return user;
        }
        async updateUser(user: User): Promise<User> {
            var user = await this.userClient.put(user.id, user);
            return user;
        }
        async deleteUser(id: number): Promise<void> {
            await this.userClient.delete(id);
        }

        async insertGroup(groupInput: GroupInput): Promise<Group> {
            var group = await this.groupClient.post(groupInput);
            return group;
        }
        async getGroups(): Promise<Group[]> {
            var groups = await this.groupClient.getAll();
            return groups;
        }
        async getGroup(id: number): Promise<Group> {
            var geoup = await this.groupClient.get(id);
            return geoup;
        }
        async updateGroup(group: Group): Promise<Group> {
            var group = await this.groupClient.put(group.id, group);
            return group;
        }
        async deleteGroup(id: number): Promise<void> {
            await this.groupClient.delete(id);
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
    }
}