import { App } from './list-Gifts';
import { expect } from 'chai';
import 'mocha';
import { IGiftClient, GiftClient, User, Gift} from "./secret-santa-api.client"

class MockGiftClient implements IGiftClient {
    async getAll(): Promise<Gift[]> {
        var user = new User({
            firstName: "Spongebob",
            lastName: "Squarepants",
            id: 42,
            santaId: null,
            gifts: null,
            groups: null
        });
        let gifts: Gift[];
        for (var i = 0; i < 5; i++) {
            gifts[i] = new Gift({
                title: "LeSpatula",
                description: "Le Spatula 3000",
                id: i + 1,
                url: "https://spongebob.fandom.com/wiki/Le_Spatula",
                userId: user.id
            });    
        }
        return gifts;
    }
    post(entity: import("./secret-santa-api.client").GiftInput): Promise<import("./secret-santa-api.client").Gift> {
        throw new Error("Method not implemented.");
    }
    get(id: number): Promise<import("./secret-santa-api.client").Gift> {
        throw new Error("Method not implemented.");
    }
    put(id: number, value: import("./secret-santa-api.client").GiftInput): Promise<import("./secret-santa-api.client").Gift> {
        throw new Error("Method not implemented.");
    }
    delete(id: number): Promise<void> {
        throw new Error("Method not implemented.");
    }

}