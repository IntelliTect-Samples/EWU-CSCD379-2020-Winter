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
                dbContext.Users.Add(SampleData.CreateSampleUser());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(SampleData.FirstName, users[0].FirstName);
                Assert.AreEqual(SampleData.LastName, users[0].LastName);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataAddedOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.NameIdentifier));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(SampleData.CreateSampleUser());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(SampleData.NameIdentifier, users[0].CreatedBy);
                Assert.AreEqual(SampleData.NameIdentifier, users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataUpdateOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.NameIdentifier));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(SampleData.CreateSampleUser());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                    hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "pbuttercup"));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                users[0].FirstName = "Princess";
                users[0].LastName = "Buttercup";

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(SampleData.NameIdentifier, users[0].CreatedBy);
                Assert.AreEqual("pbuttercup", users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_CanBeJoinedToGroup()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.NameIdentifier));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var group = SampleData.CreateSampleGroup();
                var user = SampleData.CreateSampleUser();
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
                Assert.AreEqual(SampleData.GroupTitle, users[0].UserGroups[0].Group.Title);
            }
        }

        [TestMethod]
        public async Task User_CanBeHaveGifts()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.NameIdentifier));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var gift1 = SampleData.CreateSampleGift();
                var gift2 = SampleData.CreateSampleGift();
                var user = SampleData.CreateSampleUser();
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
