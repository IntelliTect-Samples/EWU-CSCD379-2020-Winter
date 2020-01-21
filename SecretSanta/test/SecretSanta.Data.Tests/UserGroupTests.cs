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
    public class UserGroupTests : TestBase
    {
        private readonly User _User = new User
        {
            FirstName = "caleb",
            LastName = "walsh",
            UserGroups = new List<UserGroup>()
        };

        private readonly Group _Group1 = new Group { Name = "group1" };
        private readonly Group _Group2 = new Group { Name = "group2" };

        private readonly Gift _Gift = new Gift
        {
            Title = "gift",
            Description = "description",
            Url = "www.url.com"
        };

        [TestMethod]
        public async Task Create_UserWithManyGroups()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
               hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "caleb"));

            var userGroup1 = new UserGroup { User = _User, Group = _Group1 };
            var userGroup2 = new UserGroup { User = _User, Group = _Group2 };

            var userGroups = new List<UserGroup> { userGroup1, userGroup2 };

            _Gift.User = _User;
            _User.Gifts = new List<Gift> { _Gift };
            _User.UserGroups = userGroups;

            //_User.UserGroups.Add(new UserGroup { Group = _Group1, User = _User });
            //_User.UserGroups.Add(new UserGroup { Group = _Group2, User = _User });

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(_User);
                await dbContext.SaveChangesAsync();
            }


            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedUsers = await dbContext.Users.Where(u => u.Id == _User.Id).Include(u => u.Gifts)
                    .Include(u => u.UserGroups).ThenInclude(g => g.Group).SingleOrDefaultAsync();

                var giftOnUser = retrievedUsers.Gifts.ElementAt(0);

                Assert.IsNotNull(retrievedUsers);
                Assert.AreEqual(_User.FirstName, retrievedUsers.FirstName);
                Assert.AreEqual(_User.LastName, retrievedUsers.LastName);
                Assert.AreEqual(_User.Id, retrievedUsers.Id);
                Assert.IsNotNull(_User.UserGroups[0]);
                Assert.IsNotNull(_User.UserGroups[1]);
                Assert.AreEqual(userGroups[0].GroupId, retrievedUsers.UserGroups[0].GroupId);
                Assert.AreEqual(userGroups[0].UserId, retrievedUsers.UserGroups[0].UserId);
                Assert.AreEqual(userGroups[1].GroupId, retrievedUsers.UserGroups[1].GroupId);
                Assert.AreEqual(userGroups[1].UserId, retrievedUsers.UserGroups[1].UserId);
                Assert.AreEqual(1, retrievedUsers.Gifts.Count);
                Assert.AreEqual(_Gift.Url, giftOnUser.Url);
                Assert.AreEqual(_Gift.Title, giftOnUser.Title);
                Assert.AreEqual(_Gift.Description, giftOnUser.Description);
            }
        }

        [TestMethod]
        public async Task CreateGroupWithManyUsers()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            var group = new Group();
            
           
            var user1 = new User
            {
                FirstName = "Inigo",
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };
            var user2 = new User
            {
                FirstName = "Billy",
                LastName = "Bob",
                Gifts = new List<Gift>()
            };

            // Act
            
            group.UserGroups = new List<UserGroup>();
            group.UserGroups.Add(new UserGroup { Group = group, User = user1 });
            group.UserGroups.Add(new UserGroup { Group = group, User = user2 });

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Groups.Add(group);
                await dbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedPost = await dbContext.Groups.Where(p => p.Id == group.Id)
                    .Include(p => p.UserGroups).ThenInclude(pt => pt.User).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedPost);
                Assert.AreEqual(2, retrievedPost.UserGroups.Count);
                Assert.IsNotNull(retrievedPost.UserGroups[0].User);
                Assert.IsNotNull(retrievedPost.UserGroups[1].User);
            }
        }
    }
}
