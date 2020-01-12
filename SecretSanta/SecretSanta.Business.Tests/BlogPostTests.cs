using System;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CA1707 // doesn't apply to test
namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class BlogPostTests
    {
        [TestMethod]
        public void Create_BlogPost_Success()
        {
            // arrange
            const string title = "Sample Blog Post";
            const string content = "Hello my name is Inigo Montaya you killed my father prepare to die";
            const string author = "Inigo Montoya";

            // act
            BlogPost blogPost = new BlogPost(
                title,
                content,
                time:DateAndTime.Now,
                author);

            // assert
            Assert.AreEqual<string>(blogPost.Title, title);
            Assert.AreEqual<string>(blogPost.Content, content);
            Assert.AreEqual<string>(blogPost.Author, author);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_VerifyPropertiesAreNotNull_NotNull()
        {
            BlogPost blogPost = new BlogPost(null!, "", DateTime.Now, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Properties_AssignNull_ThrowArgumentNullException()
        {
            BlogPost blogPost = new BlogPost("", "", DateTime.Now, "");
            blogPost.Content = null!;
        }
    }
}
#pragma warning restore CA1707