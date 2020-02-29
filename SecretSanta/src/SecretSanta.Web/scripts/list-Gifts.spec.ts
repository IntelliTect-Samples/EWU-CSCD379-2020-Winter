
import { expect } from 'chai';
import 'mocha';
import { GiftClient, Gift, GiftInput, User, IGiftClient } from "./secretsanta-client"
import { App } from './list-Gifts';


describe('AllGifts', () => {
    it('returns gifts', async () => {
        const app = new App(new MockGiftClient());
        const actual = await app.getAllGifts();
        expect(actual.length).to.equal(10);
    });
});

describe('AddGifts', () => {
    it('adds a gift', async () => {
        let mockClient = new MockGiftClient();
        ;
        const app = new App(mockClient);
        let gifts = mockClient.gifts;
        let g = new GiftInput({
            title: "Cylon Detector",
            description: "Version 1",
            url: "findacylon.com",
            userId: mockClient.user.id
        });
        app.giftClient.post(g);
        const actual = await app.getAllGifts();
        expect(actual.length).to.equal(10);
    });
});

class MockGiftClient implements IGiftClient {

    gifts: Gift[];
    user: User;

    constructor() {
        this.user = new User({
            firstName: "Lee",
            lastName: "Adama",
            gifts: null,
            groups: null,
            id: 1
        });
        this.gifts = [];
        for (var i = 0; i < 5; i++) {
            this.gifts.push(new Gift({
                title: "Viper",
                description: "Fast Spaceship",
                url: "www.vipers.com",
                userId: this.user.id,
                id: i
            }));
        }
        for (var i = 0; i < 5; i++) {
            this.gifts.push(new Gift({
                title: "Cylon Detector",
                description: "Version 1",
                url: "www.findacylon.com",
                userId: this.user.id,
                id: i + 5
            }));
        }
    }

    search(searchTerm: string): Promise<Gift[]> {
        throw new Error("Method not implemented.");
    }
    async getAll(): Promise<Gift[]> {
        return this.gifts;
    }
    async post(entity: GiftInput): Promise<Gift> {
        let g = new Gift({
            title: entity.title,
            description: entity.description,
            url: entity.url,
            userId: this.user.id,
            id: this.gifts[this.gifts.length - 1].id + 1
        });
        this.gifts.push(g);
        return this.gifts.pop();
    }
    get(id: number): Promise<Gift> {
        throw new Error("Method not implemented.");
    }
    put(id: number, value: GiftInput): Promise<Gift> {
        throw new Error("Method not implemented.");
    }
    delete(id: number): Promise<void> {
        throw new Error("Method not implemented.");
    }
}