using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public void User_CanBeCreate_AllPropertiesGetSet()
        {
            // Arrange
            User user = new User{Id = 1, FirstName = "Inigo", LastName = "Montoya", Gifts = new List<Gift>()};

            // Act
            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Inigo", user.FirstName);
            Assert.AreEqual("Montoya", user.LastName);
            Assert.IsNotNull(user.Gifts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetFirstNameToNull_ThrowsArgumentNullException()
        {
            User user = new User {Id = 1, FirstName = null!, LastName = "Montoya", Gifts = new List<Gift>()};
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetLastNameToNull_ThrowsArgumentNullException()
        {
            User user = new User {Id = 1, FirstName = "Inigo", LastName = null!, Gifts = new List<Gift>()};
        }
        [TestMethod]
        public async Task CreateAuthor_ShouldSaveIntoDatabase()
        {
            int authorId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    Gifts = new List<Gift>()
                };
                applicationDbContext.Users.Add(user);

                var user2 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    Gifts = new List<Gift>()
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                authorId = user.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var author = await applicationDbContext.Users.Where(a => a.Id == authorId).SingleOrDefaultAsync();

                Assert.IsNotNull(author);
                Assert.AreEqual("Inigo", author.FirstName);
            }
        }
    }
}
