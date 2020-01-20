using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public void Gift_CanBeCreate_AllPropertiesGetSet()
        {
            // Arrange
            User user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                Gifts = new List<Gift>()

            };
            Gift gift = new Gift
            {
                Title = "Ring 2",
                Description = "Amazing way to keep the creepers away",
                Url = "www.ring.com",
                User = user
            };



            // Act

            // Assert
            Assert.AreEqual(1, gift.Id);
            Assert.AreEqual("Ring 2", gift.Title);
            Assert.AreEqual("Amazing way to keep the creepers away", gift.Description);
            Assert.AreEqual("www.ring.com", gift.Url);
            Assert.IsNotNull(gift.User);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            _ = new Gift
            {
                Title = null!,
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            _ = new Gift
            {
                Description = null!
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            _ = new Gift
            {
                Url = null!
            };
        }

        [TestMethod]
        public async Task AddGift_ToSpongebob_ShouldExistInDatabase()
        {
            //Arrange
            Gift gift = new Gift()
            {
                Id = 1,
                Title = "Spatula"
            };
            _Spongebob.Gifts.Add(gift);
            //Act
            using(ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                applicationDbContext.Users.Add(_Spongebob);
                await applicationDbContext.SaveChangesAsync();
            }
            //Assert
            using(ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                List<User> users = await applicationDbContext.Users.Include(u => u.Gifts).ToListAsync();
                User user = users.Where(u => u.Id == _Spongebob.Id).FirstOrDefault();
                Assert.IsNotNull(user);
                Assert.AreEqual<int>(gift.Id, user.Gifts.ElementAt(0).Id);
                Assert.AreEqual<string>(gift.Title, user.Gifts.ElementAt(0).Title);
            }
        }
    }
}
