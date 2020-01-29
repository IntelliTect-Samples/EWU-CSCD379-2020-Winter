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
            Group group = SampleData.Group1;
            string title = group.Title;

            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Groups.Add(group);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                var groups = await dbContext.Groups.ToListAsync();

                Assert.AreEqual(1, groups.Count);
                Assert.AreEqual(title, groups[0].Title);
            }
        }
    }
}
