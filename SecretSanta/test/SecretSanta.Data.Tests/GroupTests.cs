using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GroupTests : TestBase
    {
        [TestMethod]
        public async Task AddGroup_ToDatabase_ShouldExistInDatabase()
        {
            //Arrange
            //Act
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                applicationDbContext.Groups.Add(_JellyFishGroup);
                await applicationDbContext.SaveChangesAsync();
            }
            //Assert
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                List<Group> groups = await applicationDbContext.Groups.ToListAsync();
                Group group = groups.ElementAt(0);
                Assert.IsNotNull(group);
                Assert.AreEqual<int>(_JellyFishGroup.Id, group.Id);
                Assert.AreEqual<string>(_JellyFishGroup.Name, group.Name);
            }
        }
    }
}
