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

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task User_CanBeCreate_AllPropertiesGetSet()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>
                (hca => hca.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            User user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };

            // Act
            using(var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            // Assert
            using(var dbContext = new ApplicationDbContext(Options))
            {
                User retrievedUser = await dbContext.Users.Where(u => u.Id == user.Id).SingleOrDefaultAsync();
                Assert.AreEqual(1, user.Id);
                Assert.AreEqual("Inigo", user.FirstName);
                Assert.AreEqual("Montoya", user.LastName);
                Assert.IsNotNull(user.Gifts);
                Assert.IsNotNull(user.CreatedOn);
                Assert.AreEqual("imontoya", user.CreatedBy);
                Assert.AreEqual("imontoya", user.ModifiedBy);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetFirstNameToNull_ThrowsArgumentNullException()
        {
            var _ = new User
            {
                Id = 1,
                FirstName = null!,
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetLastNameToNull_ThrowsArgumentNullException()
        {
            var _ = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = null!,
                Gifts = new List<Gift>()
            };
        }
    }
}
