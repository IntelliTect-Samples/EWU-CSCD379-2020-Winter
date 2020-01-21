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
                User = user,
                Id = 1
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
        public async Task AddGift_ToDatabase_ShouldExistInDatabase()
        {
            //Arrange
           
            //Act
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                applicationDbContext.Gifts.Add(_Gift);
                await applicationDbContext.SaveChangesAsync();
            }
            //Assert
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                List<Gift> gifts = await applicationDbContext.Gifts.ToListAsync();
                Assert.IsNotNull(gifts.ElementAt(0));
                Assert.AreEqual(_Gift.Id, gifts.ElementAt(0).Id);
            }
        }

        [TestMethod]
        public async Task AddGift_ToSpongebob_ShouldCreateForeignRelationship()
        {
            //Arrange
            _Gift.User = _Spongebob;
            //Act
            using(ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                applicationDbContext.Gifts.Add(_Gift);
                await applicationDbContext.SaveChangesAsync();
            }
            //Assert
            using(ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                List<Gift> gifts = await applicationDbContext.Gifts.Include(g => g.User).ToListAsync();
                Gift gift = gifts.Where(g => g.Id == _Gift.Id).FirstOrDefault();
                Assert.IsNotNull(gift);
                Assert.AreEqual<string>(_Gift.Title, gift.Title);
                Assert.AreEqual<int>(_Gift.User.Id, gift.User.Id);
            }
        }
    }
}
