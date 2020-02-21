import { hello, App } from "./app";

import { expect } from "chai";

import "mocha";

import { IPostClient, Post, PostInput, Author } from "./blog-engine-api.client";

describe("Hello function", () => {
  it("should return hello world", () => {
    const result = hello();
    expect(result).to.equal("Hello world!");
  });
});

describe("GetAllPosts", () => {
  it("return all posts", async () => {
    const app = new App(new MockPostClient());
    const actual = await app.getAllPosts();
    expect(actual.length).to.equal(1);
  });
});

class MockPostClient implements IPostClient {
  async getAll(): Promise<Post[]> {
    var author = new Author({
      firstName: "Inigo",
      lastName: "Montoya",
      email: "ms@apple.com",
      id: 42
    });
    //return Promise.resolve([]) when not using async
    return [
      new Post({
        title: "This is the title",
        content: "This is a master work.",
        author: author,
        authorId: author.id,
        id: 77
      })
    ];
  }

  post(entity: PostInput): Promise<Post> {
    throw new Error("Method not implemented.");
  }

  get(id: number): Promise<void> {
    throw new Error("Method not implemented.");
  }

  put(id: number, value: PostInput): Promise<Post> {
    throw new Error("Method not implemented.");
  }

  delete(id: number): Promise<void> {
    throw new Error("Method not implemented.");
  }
}
