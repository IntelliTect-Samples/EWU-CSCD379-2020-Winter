using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<Group> service = new GroupService(dbContext, Mapper);
            Group redTeam = SampleData.CreateRedTeam;

            // Act
            await service.InsertAsync(redTeam);

            // Assert
            Assert.IsNotNull(redTeam.Id);
        }
    }
}
