import { GiftClient, Gift, UserClient, User } from './secretsanta-client';

export module App {
    export class Main {
        giftClient: GiftClient;
        userClient: UserClient;
        createdUser: User;

        constructor() {
            this.giftClient = new GiftClient();
            this.userClient = new UserClient();
        }

        async deletePosts() {
            var posts = await this.getPosts();

            for (let post of posts) {
                await this.giftClient.delete(post.id);
            }
        }

        async createGifts() {
            for (let i = 0; i < 5; i++) {
                let gift = new Gift();
                gift.title = `Title ${i}`;
                gift.description = `Content ${i}`;
                gift.userId = this.createdUser.id;

                await this.giftClient.post(gift);
            }
        }

        async getPosts() {
            let posts = await this.giftClient.getAll();

            return posts;
        }

        async createUser() {
            let users = await this.userClient.getAll();

            if (users.length > 0) {
                this.createdUser = users[0];
            }
            else {
                this.createdUser = new User();
                this.createdUser.firstName = 'Kara';
                this.createdUser.lastName = 'Thrace';
                await this.userClient.post(this.createdUser);
            }
        }
    }
}