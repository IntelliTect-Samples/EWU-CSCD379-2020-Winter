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
    public class GroupDataTests : TestBase
    {
        [TestMethod]
        public async Task CreateGroupsWithManyUsers()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>
                (hca => hca.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            Group group1 = new Group
            {
                Name = "Group 1"
            };

            Group group2 = new Group
            {
                Name = "Group 2"
            };

            User user1 = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya"
            };

            User user2 = new User
            {
                Id = 2,
                FirstName = "Princess",
                LastName = "Buttercup"
            };

            // Act
            user1.GroupData.Add(new GroupData { User = user1, Group = group1 });
            user2.GroupData.Add(new GroupData { User = user2, Group = group1 });
            user1.GroupData.Add(new GroupData { User = user1, Group = group2 });

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user1);
                dbContext.Users.Add(user2);
                await dbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedUser = await dbContext.Users.Where(p => p.Id == user1.Id)
                    .Include(p => p.GroupData).ThenInclude(pt => pt.Group).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedUser);
                Assert.AreEqual(2, retrievedUser.GroupData.Count);
                Assert.IsNotNull(retrievedUser.GroupData[0].Group);
                Assert.IsNotNull(retrievedUser.GroupData[1].Group);
            }

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedUser = await dbContext.Users.Where(p => p.Id == user2.Id)
                    .Include(p => p.GroupData).ThenInclude(pt => pt.Group).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedUser);
                Assert.AreEqual(1, retrievedUser.GroupData.Count);
                Assert.IsNotNull(retrievedUser.GroupData[0].Group);
            }
        }
    }
}
