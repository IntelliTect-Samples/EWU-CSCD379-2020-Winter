﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GroupTests : TestBase
    {
        [TestMethod]
        public async Task Group_CanBeSavedToDatabase()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Groups.Add(SampleData.CreateEnchantedForestGroup());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var groups = await dbContext.Groups.ToListAsync();

                Assert.AreEqual(1, groups.Count);
                Assert.AreEqual(SampleData.EnchantedForest, groups[0].Title);
            }
        }
    }
}
