import { expect } from "chai";
import "mocha";
import { App } from "./list-Gifts";
import { Gift, GiftInput, IGiftClient, User } from "./secretsanta-client";

class MockGiftClient implements IGiftClient {

    post(entity: GiftInput): Promise<Gift> {
        throw new Error("Not implemented");
    }

    get(id: number): Promise<Gift> {
        throw new Error("Not implemented");
    }

    put(id: number, value: GiftInput): Promise<Gift> {
        throw new Error("Not implemented");
    }

    delete(id: number): Promise<void> {
        throw new Error("Not implemented");
    }

    async getAll(): Promise<Gift[]> {
        var user = new User({
            firstName: "Jose",
            lastName: "Cuervo",
            santaId: null,
            gifts: null,
            groups: null,
            id: 1
        });

        var gifts = [];
        for (var ix = 0; ix < 10; ix++) {
            gifts[ix] = new Gift({
                title: "${ix} title",
                description: "${ix} description",
                url: "${ix} url",
                userId: 0,
                id: 0
            });
        }

        return gifts;
    }
}

describe("RetrieveGifts", () => {
    it("Gets all gifts", async () => {
        const app = new App(new MockGiftClient());
        const result = await app.getGifts();
        expect(result.length).to.equal(10);
    });
});