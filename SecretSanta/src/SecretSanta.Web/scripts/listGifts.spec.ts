import { App } from "./listGifts";

import { expect } from "chai";

import "mocha";

import {
    IGiftClient,
    Gift,
    GiftInput,
    User
} from "./secretsanta-client";

describe("RetrieveAllGifts", () => {
    it("return all gifts", async () => {
        const app = new App(new MockGiftClient());
        const actual = await app.retrieveAllGifts();
        expect(actual.length).to.equal(5);
    });
});

class MockGiftClient implements IGiftClient {
    async getAll(): Promise<Gift[]> {
        var user = new User({
            firstName: "Inigo",
            lastName: "Montoya",
            santaId: null,
            gifts: null,
            groups: null,
            id: 1
        });

        let gifts: Gift[];
        for (var i = 0; i < 5; i++) {
            gifts[i] = new Gift({
                title: "Title ${ i }",
                description: "Description ${ i }",
                url: "www.${ i }.com",
                userId: 1,
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