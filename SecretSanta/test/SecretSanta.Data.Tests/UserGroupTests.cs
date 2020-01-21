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
    class UserGroupTests : TestBase
    {
        [TestMethod]
        public async Task CreateUserWithManyGroups()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            var user = new User
            {
                FirstName = "Inigo",
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };
            
            var group1 = new Group("group1");
            var group2 = new Group("group2");

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
                var retrievedPost = await dbContext.Users.Where(p => p.Id == user.Id)
                    .Include(p => p.UserGroups).ThenInclude(pt => pt.Group).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedPost);
                Assert.AreEqual(2, retrievedPost.UserGroups.Count);
                Assert.IsNotNull(retrievedPost.UserGroups[0].Group);
                Assert.IsNotNull(retrievedPost.UserGroups[1].Group);
            }
        }

        [TestMethod]
        public async Task CreateGroupWithManyUsers()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            var group = new Group("group");
            
           
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
