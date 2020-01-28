using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Data.Tests
{
    [TestClass]
    public class PostTests : TestBase
    {
        [TestMethod]
        public async Task AddPost_WithAuthor_ShouldCreateForeignRelationship()
        {
            var author = new Author("Inigo", "Montoya", "inigo@montoya.me");

            var post = new Post("My Title", "Here is some basic content", author)
            { 
                Slug = "my-title"
            };
            // Arrange
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                post.Author = author;

                dbContext.Posts.Add(post);

                await dbContext.SaveChangesAsync();
            }

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            { 
                var posts = await dbContext.Posts.Include(p => p.Author).ToListAsync();
                //var posts = await dbContext.Posts.ToListAsync();
                Assert.AreEqual(1, posts.Count);
                Assert.AreEqual(post.Title, posts[0].Title);
                Assert.IsNotNull(posts[0].Author);
                Assert.AreNotEqual(0, posts[0].Author.Id);
            }
        }
    }
}
