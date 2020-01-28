using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
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
                dbContext.Groups.Add(SampleData.CreateGroup1());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var groups = await dbContext.Groups.ToListAsync();

                Assert.AreEqual(1, groups.Count);
                Assert.AreEqual(SampleData.GroupTitle1, groups[0].Title);
            }
        }
    }
}
