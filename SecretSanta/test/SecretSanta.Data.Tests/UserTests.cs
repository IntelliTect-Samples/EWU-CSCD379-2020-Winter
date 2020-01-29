using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task User_CanSaveToDatabase()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Users.Add(SampleData.CreateInigoMontoya());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual<int>(1, users.Count);
                Assert.AreEqual<string>(SampleData.Inigo, users[0].FirstName);
                Assert.AreEqual<string>(SampleData.Montoya, users[0].LastName);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataAddedOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(SampleData.CreateInigoMontoya());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual<int>(1, users.Count);
                Assert.AreEqual<string>("imontoya", users[0].CreatedBy!);
                Assert.AreEqual<string>("imontoya", users[0].ModifiedBy!);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataUpdateOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(SampleData.CreateInigoMontoya());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                    hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "pbuttercup"));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                users[0].FirstName = "Princess";
                users[0].LastName = "Buttercup";

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual("imontoya", users[0].CreatedBy);
                Assert.AreEqual("pbuttercup", users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_CanBeJoinedToGroup()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var group = new Group("Enchanted Forest");
                var user = SampleData.CreateInigoMontoya();
                user.UserGroups.Add(new UserGroup { User = user, Group = group });
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.Include(u => u.UserGroups).ThenInclude(ug => ug.Group).ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(1, users[0].UserGroups.Count);
                Assert.AreEqual("Enchanted Forest", users[0].UserGroups[0].Group.Title);
            }
        }

        [TestMethod]
        public async Task User_CanBeHaveGifts()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var gift1 = SampleData.CreateGift1();
                var gift2 = SampleData.CreateGift2();
                var user = SampleData.CreateInigoMontoya();
                user.Gifts.Add(gift1);
                user.Gifts.Add(gift2);
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.Include(u => u.Gifts).ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(2, users[0].Gifts.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetFirstNameToNull_ThrowsArgumentNullException()
        {
            _ = new User(null!, "Montoya");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetLastNameToNull_ThrowsArgumentNullException()
        {
            _ = new User("Inigo", null!);
        }
    }
}
