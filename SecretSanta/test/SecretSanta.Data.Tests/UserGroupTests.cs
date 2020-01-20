using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserGroupTests : TestBase
    {
        [TestMethod]
        public async Task CreateGroupsWithManyUsers()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            var group1 = new Group
            {
                Name = "Pen Pals"
            };
            var group2 = new Group()
            {
                Name = "Cheesy Chess"
            };
            var user1 = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya"
            };
            var user2 = new User
            {
                Id = 2,
                FirstName = "Pirate",
                LastName = "Roberts"
            };

            // Act
            user1.UserGroups = new List<UserGroup>();
            user2.UserGroups = new List<UserGroup>();
            user1.UserGroups.Add(new UserGroup{User = user1, Group=group1});
            user2.UserGroups.Add(new UserGroup{User = user2, Group = group1});
            user2.UserGroups.Add(new UserGroup{User=user2, Group=group2});

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user1);
                dbContext.Users.Add(user2);
                await dbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedUser = await dbContext.Users.Where(u => u.Id == user1.Id)
                    .Include(u => u.UserGroups).ThenInclude(ug => ug.Group).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedUser);
                Assert.AreEqual(1, retrievedUser.UserGroups.Count);
                Assert.IsNotNull(retrievedUser.UserGroups[0].Group);
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedUser = await dbContext.Users.Where(u => u.Id == user2.Id)
                    .Include(u => u.UserGroups).ThenInclude(ug => ug.Group).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedUser);
                Assert.AreEqual(2, retrievedUser.UserGroups.Count);
                Assert.IsNotNull(retrievedUser.UserGroups[0].Group);
                Assert.IsNotNull(retrievedUser.UserGroups[1].Group);
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedGroup = await dbContext.Groups.Where(g => g.Id == group1.Id)
                    .Include(g => g.UserGroups).ThenInclude(ug => ug.Group).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedGroup);
                Assert.AreEqual(2, retrievedGroup.UserGroups.Count);
                Assert.IsNotNull(retrievedGroup.UserGroups[0].Group);
                Assert.IsNotNull(retrievedGroup.UserGroups[1].Group);
            }
        }
    }
}
