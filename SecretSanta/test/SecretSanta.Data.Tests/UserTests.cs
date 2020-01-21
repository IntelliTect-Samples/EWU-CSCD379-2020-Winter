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
        public async Task User_CreateUser_SavesInDatabase()
        {
            int userId = -1;

            // Arrange
            using(var applicationDbContext = new ApplicationDbContext(Options))
            {
                var testUser = new User
                {
                    FirstName = "Jerett",
                    LastName = "Latimer"
                };
                applicationDbContext.Users.Add(testUser);

                var testUser2 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };
                applicationDbContext.Users.Add(testUser2);

                await applicationDbContext.SaveChangesAsync();

                userId = testUser.Id;

            }

            // Act
            // Assert
            using(var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                Assert.IsNotNull(user);
                Assert.AreEqual("Jerett", user.FirstName);
                Assert.AreEqual("Latimer", user.LastName);
            }
        }

        [TestMethod]
        public async Task User_CreateUser_SetsFingerPrintDataOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "jlatimer"));

            int userId = -1;

            // Arrange
            using(var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = new User
                {
                    FirstName = "Jerett",
                    LastName = "Latimer"
                };
                applicationDbContext.Add(user);

                var user2 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };
                applicationDbContext.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user.Id;
            }
            // Act
            // Assert
            using(var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("jlatimer", user.CreatedBy);
                Assert.AreEqual("jlatimer", user.ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_CreateUser_SetsFingerPrintDataOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "jlatimer"));

            int userId = -1;

            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = new User
                {
                    FirstName = "Jerett",
                    LastName = "Latimer"
                };
                applicationDbContext.Add(user);

                var user2 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };
                applicationDbContext.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user.Id;
            }
            // Act
            // Change user
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "notjlatimer"));
            using(var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                user.FirstName = "notJerett";
                user.LastName = "notJerett";

                await applicationDbContext.SaveChangesAsync();
            }

            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("jlatimer", user.CreatedBy);
                Assert.AreEqual("notjlatimer", user.ModifiedBy);
            }
        }
    }
}
