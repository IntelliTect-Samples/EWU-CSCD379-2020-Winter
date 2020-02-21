import {
  IAuthorClient,
  IPostClient,
  PostClient
} from "./blog-engine-api.client";

export const hello = () => "Hello world!";

export class App {
  async renderPosts() {
    var posts = await this.getAllPosts();
    const itemList = document.getElementById("itemList");
    for (let index = 0; index < posts.length; index++) {
      const post = posts[index];
      //document.write("Hello World");
      const listItem = document.createElement("li");
      listItem.textContent = `${post.title}:${post.content}`;
      itemList.append(listItem);
    }
  }

  postClient: IPostClient;
  constructor(postClient: IPostClient = new PostClient()) {
    this.postClient = postClient;
  }

  async getAllPosts() {
    var posts = await this.postClient.getAll();
    return posts;
  }
}
