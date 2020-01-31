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
                dbContext.Users.Add(SampleData.CreateUserGaiusBaltar());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(SampleData.CreateUserGaiusBaltar().FirstName, users[0].FirstName);
                Assert.AreEqual(SampleData.CreateUserGaiusBaltar().LastName, users[0].LastName);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataAddedOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dsergio"));

            // Arrange
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(SampleData.CreateUserKaraThrace());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                List<User> users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual("dsergio", users[0].CreatedBy);
                Assert.AreEqual("dsergio", users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataUpdateOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dsergio"));

            // Arrange
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(SampleData.CreateUserLeeAdama());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                    hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "number6"));

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                List<User> users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                users[0].FirstName = "Number";
                users[0].LastName = "6";

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual("dsergio", users[0].CreatedBy);
                Assert.AreEqual("number6", users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_CanBeJoinedToGroup()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dsergio"));

            // Arrange
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var group = SampleData.CreateGroupColonialFleet();
                var user = SampleData.CreateUserLeeAdama();
                user.UserGroups.Add(new UserGroup { User = user, Group = group });
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                List<User> users = await dbContext.Users.Include(u => u.UserGroups).ThenInclude(ug => ug.Group).ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(1, users[0].UserGroups.Count);
                Assert.AreEqual(SampleData.CreateGroupColonialFleet().Title, users[0].UserGroups[0].Group.Title);
            }
        }

        [TestMethod]
        public async Task User_CanBeHaveGifts()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dsergio"));

            // Arrange
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                Gift gift1 = SampleData.CreateGiftFTLDrive();
                Gift gift2 = SampleData.CreateGiftViper();
                User user = SampleData.CreateUserGaiusBaltar();
                user.Gifts.Add(gift1);
                user.Gifts.Add(gift2);
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                List<User> users = await dbContext.Users.Include(u => u.Gifts).ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(2, users[0].Gifts.Count);
            }
        }

        [DataTestMethod]
        [DataRow(null!, "Roslin")]
        [DataRow("Laura", null!)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetFirstNameToNull_ThrowsArgumentNullException(string firstName, string lastName)
        {
            _ = new User(firstName, lastName);
        }
    }
}
