using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task User_CanSaveToDatabase()
        {
            User user = SampleData.User1;
            string fname = user.FirstName;
            string lname = user.LastName;

            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(fname, users[0].FirstName);
                Assert.AreEqual(lname, users[0].LastName);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataAddedOnInitialSave()
        {
            User user = SampleData.User2;
            string identifier = $"{user.LastName}, {user.FirstName}";

            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, identifier));

            

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(identifier, users[0].CreatedBy);
                Assert.AreEqual(identifier, users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_HasFingerPrintDataUpdateOnUpdate()
        {
            User user1 = SampleData.User1;
            string identifier1 = $"{user1.LastName}, {user1.FirstName}";

            User user2 = SampleData.User3;
            string fname = user2.FirstName;
            string lname = user2.LastName;
            string identifier2 = $"{user2.LastName}, {user2.FirstName}";

            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, identifier1));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user1);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                    hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, identifier2));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                users[0].FirstName = fname;
                users[0].LastName = lname;

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(identifier1, users[0].CreatedBy);
                Assert.AreEqual(identifier2, users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task User_CanBeJoinedToGroup()
        {
            User user = SampleData.User1;
            string identifier = $"{user.LastName}, {user.FirstName}";

            Group group = SampleData.Group1;
            string title = group.Title;

            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, identifier));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                user.UserGroups.Add(new UserGroup { User = user, Group = group });
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var users = await dbContext.Users.Include(u => u.UserGroups).ThenInclude(ug => ug.Group).ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual(1, users[0].UserGroups.Count);
                Assert.AreEqual(title, users[0].UserGroups[0].Group.Title);
            }
        }

        [TestMethod]
        public async Task User_CanBeHaveGifts()
        {
            User user = SampleData.User3;
            string identifier = $"{user.LastName}, {user.FirstName}";

            Gift gift1 = SampleData.Gift1User(user);
            Gift gift2 = SampleData.Gift2User(user);

            user.Gifts.Add(gift1);
            user.Gifts.Add(gift2);

            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, identifier));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

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
