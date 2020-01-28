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

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(SampleData.Inigo, users[0].FirstName);
                Assert.AreEqual(SampleData.Montoya, users[0].LastName);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataAddedOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.InigoMontoyaUsername));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(SampleData.CreateJackRyan());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(SampleData.InigoMontoyaUsername, users[0].CreatedBy);
                Assert.AreEqual(SampleData.InigoMontoyaUsername, users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataUpdateOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.JerettLatimerUsername));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(SampleData.CreateJerettLatimer());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                    hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.JackRyanUsername));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                users[0].FirstName = SampleData.Jack;
                users[0].LastName = SampleData.Ryan;

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(SampleData.JerettLatimerUsername, users[0].CreatedBy);
                Assert.AreEqual(SampleData.JackRyanUsername, users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_CanBeJoinedToGroup()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.InigoMontoyaUsername));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var group = SampleData.CreateRedTeam;
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
                Assert.AreEqual(SampleData.RedTeam, users[0].UserGroups[0].Group.Title);
            }
        }

        [TestMethod]
        public async Task User_CanHaveGifts()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, SampleData.InigoMontoyaUsername));

            // Arrange
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var gift1 = SampleData.CreateRingGift();
                var gift2 = SampleData.CreateArduinoGift();
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
                Console.WriteLine(users[0].Gifts.Count);
                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(2, users[0].Gifts.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetFirstNameToNull_ThrowsArgumentNullException()
        {
            _ = new User(null!, SampleData.Montoya);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetLastNameToNull_ThrowsArgumentNullException()
        {
            _ = new User(SampleData.Inigo, null!);
        }
    }
}
