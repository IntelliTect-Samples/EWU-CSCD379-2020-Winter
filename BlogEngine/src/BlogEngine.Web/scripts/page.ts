import { AuthorClient, PostClient } from "./blog-engine-api.client"

export const hello = () => 'Hello world!'; 


export class App 
{
    async getAllAuthors() {
        var client = new AuthorClient();
        var authors = await client.getAll();
        return authors;
    }
}


export async function getAllAuthors() {
    var client = new AuthorClient();
    var authors = await client.getAll();
    return authors;
}


