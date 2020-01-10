using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class BlogPostTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_PropertiesSetToNull_ThrowsException()
        {
            // Arrange

            // Act
            BlogPost blogPost = new BlogPost(
                null!,
                null!,
                DateTime.Now,
                null!);

            // Assert
            
        }

        [TestMethod]
        public void Create_BlogPost_Success()
        {
            // Arrange
            const string title = "Sample Blog Post";
            const string content = "Hi";
            const string author = "Author";


            // Act
            BlogPost blogPost = new BlogPost(
                title,
                content,
                DateTime.Now,
                author);

            // Assert
            Assert.AreEqual<string>(title, blogPost.Title);
            Assert.AreEqual<string>(content, blogPost.Content);
            Assert.AreEqual<string>(author, blogPost.Author);
        }
    }
}
