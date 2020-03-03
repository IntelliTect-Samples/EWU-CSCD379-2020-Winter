import { App } from "./list-Gifts";

import { expect } from "chai";

import "mocha";

import { IGiftClient, Gift, GiftInput, User } from "./secretsanta-engine-api.client";


describe("GetAllGifts", () => {
    it("return all gifts", async () => {
        const app = new App(new MockGiftClient());
        const actual = await app.getAllGifts();
        expect(actual.length).to.equal(1);
    });
});

class MockGiftClient implements IGiftClient
{
    async getAll(): Promise<Gift[]>
    {
        let gifts : Gift[];
        for (var i = 0; i < 5; i++) {
            gifts[i] = new Gift({
                title: "${i},Title ",
                description: "${i}, Description ",
                url: "www.website.com",
                userId: 22,
                id: i
            })
        }
        
       
        return gifts;
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