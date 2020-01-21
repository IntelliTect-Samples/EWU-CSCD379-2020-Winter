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
            User user = new User {Id = 1, FirstName = "Inigo", LastName = "Montoya", Gifts = new List<Gift>()};

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
        public async Task CreateUser_ShouldSaveIntoDatabase()
        {
            int userId = -1;
            using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
            {
                User user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    Gifts = new List<Gift>()
                };
                applicationDbContext.Users?.Add(user);

                await applicationDbContext.SaveChangesAsync();
                userId = user.Id;
            }

            // Act

            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(i => i.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("Inigo", user.FirstName);
            }
        }
    }
}
