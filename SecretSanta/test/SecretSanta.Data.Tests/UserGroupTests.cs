using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserGroupTests : TestBase
    {
        [TestMethod]
        public async Task CreateUserGroup_WithManyGroups_DbAllPropertiesGetSet()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                CreatedBy = "imont",
                Santa = null,
                Gifts = new List<Gift>(),
                UserGroups = new List<UserGroup>()
            };

            var gift = new Gift
            {
                Id = 1,
                Title = "Some Gift",
                Description = "A random gift",
                Url = "www.thegift.com",
                CreatedBy = "imont"
            };

            Group group1 = new Group
            {
                Name = "Group1"
            };

            Group group2 = new Group
            {
                Name = "Group2"
            };

            //Act
            user.UserGroups = new List<UserGroup>
            {
                new UserGroup { User = user, Group = group1 },
                new UserGroup { User = user, Group = group2 }
            };

            gift.User = user;

            // Act
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(gift);
                await dbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                var dbGift = await dbContext.Gifts.Where(g => g.Id == gift.Id)
                    .Include(u => u.User)
                    .ThenInclude(ug => ug.UserGroups)
                    .ThenInclude(gr => gr.Group)
                    .SingleOrDefaultAsync();

                Assert.IsNotNull(dbGift);
                Assert.AreEqual(2, dbGift.User.UserGroups.Count);
                Assert.IsNotNull(dbGift.User.UserGroups[0].Group);
                Assert.IsNotNull(dbGift.User.UserGroups[1].Group);
            }
        }
    }
}
