import { hello, ListGifts } from './List-Gifts';
import { expect } from 'chai';
import 'mocha'
import { IGiftClient, Gift, GiftInput, User } from './secret-santa-api.client';

describe('Hello function', () => {
    it('should return hello world',
        () => {
            const result = hello();
            expect(result).to.equal('Hello world!');
        });
});

describe('GetAllGifts', () => {
    it('return all gifts', async () => {
        const app = new ListGifts(new MockGiftClient());
        const actual = await app.getAllGifts();
        expect(actual.length).to.equal(1);
    })
})

class MockGiftClient implements IGiftClient {

    async getAll(): Promise<Gift[]> {
        var gift = new Gift({
            title: "Fries",
            description: "Potatoes that are deep-fried",
            url: "fries.io",
            id: 4,
            userId: 1
        });
        return [gift];
    }    
    post(entity: GiftInput): Promise<Gift> {
        throw new Error("Method not implemented.");
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