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
        [TestMethod]
        public async Task CreateUser_WithManyGroups()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "Tester"));

            var user = new User
            {
                FirstName = "TestFirst",
                LastName = "TestLast"
            };

            var group1 = new Group
            {
                Name = "TestGroup1"
            };
            var group2 = new Group
            {
                Name = "TestGroup2"
            };

            // Act
            user.UserGroups = new List<UserGroup>();
            user.UserGroups.Add(new UserGroup { User = user, Group = group1 });
            user.UserGroups.Add(new UserGroup { User = user, Group = group2 });

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedUser = await dbContext.Users.Where(u => u.Id == user.Id)
                    .Include(u => u.UserGroups).ThenInclude(ug => ug.Group).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedUser);
                Assert.AreEqual<int>(2, retrievedUser.UserGroups.Count);
                Assert.IsNotNull(retrievedUser.UserGroups[0].Group);
                Assert.IsNotNull(retrievedUser.UserGroups[1].Group);
                Assert.AreEqual<string>("TestGroup1", retrievedUser.UserGroups[0].Group.Name);
                Assert.AreEqual<string>("TestGroup2", retrievedUser.UserGroups[1].Group.Name);
            }
        }

        [TestMethod]
        public async Task CreateGroup_WithManyUsers()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "Tester"));

            var group = new Group
            {
                Name = "TestGroup"
            };

            var user1 = new User
            {
                FirstName = "TestFirst1",
                LastName = "TestLast1"
            };
            var user2 = new User
            {
                FirstName = "TestFirst2",
                LastName = "TestLast2"
            };

            // Act
            group.UserGroups = new List<UserGroup>();
            group.UserGroups.Add(new UserGroup { User = user1, Group = group });
            group.UserGroups.Add(new UserGroup { User = user2, Group = group });

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Groups.Add(group);
                await dbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedGroup = await dbContext.Groups.Where(g => g.Id == group.Id)
                    .Include(g => g.UserGroups).ThenInclude(ug => ug.User).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedGroup);
                Assert.AreEqual<int>(2, retrievedGroup.UserGroups.Count);
                Assert.IsNotNull(retrievedGroup.UserGroups[0].User);
                Assert.IsNotNull(retrievedGroup.UserGroups[1].User);
                Assert.AreEqual<string>("TestFirst1", retrievedGroup.UserGroups[0].User.FirstName);
                Assert.AreEqual<string>("TestLast1", retrievedGroup.UserGroups[0].User.LastName);
                Assert.AreEqual<string>("TestFirst2", retrievedGroup.UserGroups[1].User.FirstName);
                Assert.AreEqual<string>("TestLast2", retrievedGroup.UserGroups[1].User.LastName);
            }
        }
    }
}
