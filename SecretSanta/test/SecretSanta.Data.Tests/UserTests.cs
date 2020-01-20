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
        public void User_CanBeCreate_AllPropertiesGetSet()
        {
            // Arrange
            User user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };

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
            _ = new User
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
            _ = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = null!,
                Gifts = new List<Gift>()
            };
        }

        [TestMethod]
        public async Task CreateUser_ShouldSaveIntoDatabase()
        {
            int userId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user1 = new User
                {
                    FirstName = "TestFirst1",
                    LastName = "TestLast1"
                };
                applicationDbContext.Users.Add(user1);

                var user2 = new User
                {
                    FirstName = "TestFirst2",
                    LastName = "TestLast2"
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user1.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual<string>("TestFirst1", user.FirstName);
                Assert.AreEqual<string>("TestLast1", user.LastName);
            }
        }

        [TestMethod]
        public async Task CreateUser_ShouldSetFingerPrintDataOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "Tester"));

            int userId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user1 = new User
                {
                    FirstName = "TestFirst1",
                    LastName = "TestLast1"
                };
                applicationDbContext.Users.Add(user1);

                var user2 = new User
                {
                    FirstName = "TestFirst2",
                    LastName = "TestLast2"
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user1.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("Tester", user.CreatedBy);
                Assert.AreEqual("Tester", user.ModifiedBy);
            }
        }

        [TestMethod]
        public async Task CreateUser_ShouldSetFingerPrintDataOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "Tester"));

            int userId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user1 = new User
                {
                    FirstName = "TestFirst1",
                    LastName = "TestLast1",
                };
                applicationDbContext.Users.Add(user1);

                var user2 = new User
                {
                    FirstName = "TestFirst2",
                    LastName = "TestLast2",
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user1.Id;
            }

            // Act
            // change the user that is updating the record
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "NewTester"));
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                // Since we are pulling back the record from the database and making changes to it, we don't need to re-add it to the collection
                // thus no Users.Add call, that is only needed when new records are inserted
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                user.FirstName = "NewFirst";
                user.LastName = "NewLast";

                await applicationDbContext.SaveChangesAsync();
            }
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("Tester", user.CreatedBy);
                Assert.AreEqual("NewTester", user.ModifiedBy);
                Assert.AreEqual("NewFirst", user.FirstName);
            }
        }
    }
}
