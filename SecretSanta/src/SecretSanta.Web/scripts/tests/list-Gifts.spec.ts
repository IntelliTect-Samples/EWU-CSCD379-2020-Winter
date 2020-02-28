import { assert } from "chai";
import "mocha";
import { IGiftClient, Gift, GiftInput, User } from "../secretsanta-client";

describe("list-Gifts.ts", () => {
    it("generateAllGifts", async () => {
        const app: MockGiftClient = new MockGiftClient();
        const actual: Gift[] = await app.getAll();
        assert.equal(actual.length, 5);
    });
});

class MockGiftClient implements IGiftClient {
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

    async getAll(): Promise<Gift[]> {
        let gifts: Gift[] = [];
        for (let index: number = 0; index < 5; index++) {
            gifts[index] = new Gift({
                title: `<Title #${index}>`,
                description: `<Description #${index}>`,
                url: "http://www.gabeRocks.com",
                userId: 1,
                id: index
            })
        }
        return gifts;
    }
} 