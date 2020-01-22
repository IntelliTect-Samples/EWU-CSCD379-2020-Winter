using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Data.Tests
{

    [TestClass]
    public class GiftTests : TestBase
    {

        [TestMethod]
        public void Constructor_GivenValidData_NotNull()
        {
            var gift = new Gift
            {
                Id          = 1,
                Title       = "Ring 2",
                Description = "Amazing way to keep the creepers away",
                Url         = "www.ring.com",
                User        = new User {Id = 1, FirstName = "Inigo", LastName = "Montoya", Gifts = new List<Gift>()}
            };

            Assert.IsNotNull(gift.User);
        }

        [DataTestMethod]
        [DataRow(null, "Description", "URL", false)]
        [DataRow("Title", null, "URL", false)]
        [DataRow("Title", "Description", null, false)]
        [DataRow("Title", "Description", "URL", true)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_InvalidData_ThrowsException(string title, string description, string url, bool nullUser)
        {
            // ReSharper disable once AssignmentIsFullyDiscarded
            _ = new Gift
            {
                Id          = 1,
                Title       = title,
                Description = description,
                Url         = url,
                User = nullUser
                           ? null
                           : new User {Id = 1, FirstName = "Brett", LastName = "Henning", Gifts = new List<Gift>()}
            };
        }

        [TestMethod]
        public async Task Gift_SaveToDb_RunsCorrectly()
        {
            await using (var context = new ApplicationDbContext(Options, Accessor))
            {
                var user = new User {Id = 1, FirstName = "Brett", LastName = "Henning", Gifts = new List<Gift>()};
                context.Users.Add(user);

                var gift = new Gift {Title = "Title", Description = "Description", Url = "URL", User = user};
                context.Gifts.Add(gift);

                await context.SaveChangesAsync();
            }

            await using (var context = new ApplicationDbContext(Options, Accessor))
            {
                var gifts = await context.Gifts.Include(g => g.User).ToListAsync();
                
                Assert.AreEqual(1, gifts.Count);
                var gift = gifts[0];
                
                Assert.AreEqual(gift.Title, "Title");
                Assert.AreEqual(gift.Description, "Description");
                Assert.AreEqual(gift.Url, "URL");
                Assert.AreEqual(gift.User.Id, 1);
                Assert.AreEqual(gift.User.FirstName, "Brett");
                Assert.AreEqual(gift.User.LastName, "Henning");
            }
        }

    }

}
