using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public async Task Gift_CanBeSavedToDatabase()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var testGift = SampleData.CreateViper();
                testGift.User = SampleData.CreateWilliamAdama();
                dbContext.Gifts.Add(testGift);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(SampleData.CreateViper().Title, gifts[0].Title);
                Assert.AreEqual(SampleData.CreateViper().Url, gifts[0].Url);
                Assert.AreEqual(SampleData.CreateViper().Description, gifts[0].Description);
            }
        }

        [DataTestMethod]
        [DataRow(null!, "description", "url")]
        [DataRow("title", null!, "url")]
        [DataRow("title", "description", null!)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDataToNull_ThrowsArgumentNullException(string title, string description, string url)
        {
            _ = new Gift(title, description, url, SampleData.CreateWilliamAdama());
        }

    }
}
