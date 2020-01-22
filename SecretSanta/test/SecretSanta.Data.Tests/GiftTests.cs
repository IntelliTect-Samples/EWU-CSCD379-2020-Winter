using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public async Task Gift_CanBeCreate_AllPropertiesGetSet()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));
            var user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya"
            };
            var gift = new Gift{
                Id = 1,
                Title = "Ring 2",
                Description = "Amazing way to keep the creepers away",
                Url = "www.ring.com"
            };
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                gift.User = user;

                dbContext.Gifts.Add(gift);

                await dbContext.SaveChangesAsync();
            }

            // Act

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var gifts = await dbContext.Gifts.Include(g => g.User).ToListAsync();
                //var posts = await dbContext.Posts.ToListAsync();
                Assert.AreEqual(1, gift.Id);
                Assert.AreEqual("Ring 2", gift.Title);
                Assert.AreEqual("Amazing way to keep the creepers away", gift.Description);
                Assert.AreEqual("www.ring.com", gift.Url);
                Assert.IsNotNull(gift.User);
            }
        }



        [TestMethod]
        [DataRow(1, null!, "Amazing way to keep the creepers away", "www.ring.com", "imontoya")]
        [DataRow(1, "Ring 2", null!, "www.ring.com", "imontoya")]
        [DataRow(1, "Ring 2", "Amazing way to keep the creepers away", null!, "imontoya")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetNullTests_ThrowsArgumentNullException(int id, string title, string desc, string url, string cb)
        {
            var gift = new Gift
            {
                Id = id,
                Title = title,
                Description = desc,
                Url = url,
                CreatedBy = cb
            };
        }
    }
}
