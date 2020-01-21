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
        public async Task UserGroup_CreateUserWithManyGroups_Success()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "jlatimer"));

            var gift = new Gift
            {
                Title = "Test Gift",
                Description = "Test Description",
                Url = "www.TestUrl.com"
            };

            var user = new User
            {
                FirstName = "Jerett",
                LastName = "Latimer"
            };

            var firstGroup = new Group
            {
                Name = "First Group"
            };

            var secondGroup = new Group
            {
                Name = "Second Group"
            };

            // Act
            gift.User = user;
            user.UserGroups = new List<UserGroup>();
            user.UserGroups.Add(new UserGroup { User = user, Group = firstGroup });
            user.UserGroups.Add(new UserGroup { User = user, Group = secondGroup });

            using(ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                applicationDbContext.Gifts.Add(gift);
                await applicationDbContext.SaveChangesAsync();
            }

            // Assert
            using(ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedGift = await applicationDbContext.Gifts.Where(g => g.Id == gift.Id)
                    .Include(u => u.User).ThenInclude(ug => ug.UserGroups).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedGift);
                Assert.AreEqual(2, retrievedGift.User.UserGroups.Count);
                Assert.IsNotNull(retrievedGift.User.UserGroups[0].Group);
                Assert.IsNotNull(retrievedGift.User.UserGroups[1].Group);
            }

        }
    }
}
