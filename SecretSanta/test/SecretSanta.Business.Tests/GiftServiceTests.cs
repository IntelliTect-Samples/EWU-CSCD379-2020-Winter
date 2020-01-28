using SecretSanta.Data.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class PostServiceTests : TestBase
    {
        [TestMethod]
        public async Task CreatePost_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IPostService service = new PostService(dbContext, Mapper);

            var author = new Author("Inigo", "Montoya", "inigo@montoya.me");

            var post = new Post("Title", "Content", author);

            await service.InsertAsync(post);

            // Act

            // Assert
            Assert.IsNotNull(post.Id);
            Assert.IsNotNull(author.Id);
            Assert.AreSame(post.Author, author);
            Assert.AreEqual(author.Id, post.Author.Id);
        }

        [TestMethod]
        public async Task FetchByIdPost_ShouldIncludeAuthor()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IPostService service = new PostService(dbContext, Mapper);

            var author = new Author("Inigo", "Montoya", "inigo@montoya.me");

            Post post = new Post("Title", "Content", author);

            await service.InsertAsync(post);

            // Act

            // Assert
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new PostService(dbContext, Mapper);
            post = await service.FetchByIdAsync(post.Id!.Value);

            Assert.IsNotNull(post.Author);
        }

        [TestMethod]
        public async Task UpdateAuthor_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IAuthorService service = new AuthorService(dbContext, Mapper);

            var author = new Author("Inigo", "Montoya", "inigo@montoya.me");

            var author2 = new Author("Inigo", "Montoya", "inigo@montoya.me");

            await service.InsertAsync(author);
            await service.InsertAsync(author2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            Author fetchAuthor = await dbContext2.Authors.SingleAsync(item => item.Id == author.Id);

            fetchAuthor.FirstName = "Princess";
            fetchAuthor.LastName = "Buttercup";

            using var dbContext3 = new ApplicationDbContext(Options);
            var service2 = new AuthorService(dbContext3, Mapper);
            await service2.UpdateAsync(2, fetchAuthor);

            // Assert
            using var dbContext4 = new ApplicationDbContext(Options);
            Author savedAuthor = await dbContext4.Authors.SingleAsync(item => item.Id == author.Id);
            var otherAuthor = await dbContext4.Authors.SingleAsync(item => item.Id == 2);
            Assert.AreEqual(("Inigo", "Montoya"), (savedAuthor.FirstName, savedAuthor.LastName));
            Assert.AreNotEqual((savedAuthor.FirstName, savedAuthor.LastName), (otherAuthor.FirstName, otherAuthor.LastName));

        }
    }
}
