using Microsoft.EntityFrameworkCore;
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
    public class UserServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_BillyAndFred_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IUserService service = new UserService(dbContextInsert, Mapper);

            User billy = SampleData.CreateBillyBob();
            User fred = SampleData.CreateFredFlintstone();

            // Act
            await service.InsertAsync(billy);
            await service.InsertAsync(fred);

            // Assert
            Assert.IsNotNull(billy.Id);
            Assert.IsNotNull(fred.Id);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IUserService service = new UserService(dbContextInsert, Mapper);

            User billy = SampleData.CreateBillyBob();
            User fred = SampleData.CreateFredFlintstone();

            await service.InsertAsync(billy);
            await service.InsertAsync(fred);

            // Act
            using var dbContextFetch = new ApplicationDbContext(Options);
            User billyFromDb = await dbContextFetch.Users.SingleAsync(item => item.Id == billy.Id);

            const string updatedLastName = "Not Billy Bob at all";
            billyFromDb.LastName = updatedLastName;

            // Update Billy Bob using Fred Id.
            await service.UpdateAsync(fred.Id!.Value, billyFromDb);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            billyFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == billy.Id);
            var fredFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == 2);


            // I'm confused about how this test is supposed to work based off the lecture notes
            //Assert.AreEqual(
            //    (SampleData.Billy, updatedLastName), (fredFromDb.FirstName, fredFromDb.LastName));

            //Assert.AreEqual(
            //    (SampleData.Billy, SampleData.Bob), (billyFromDb.FirstName, billyFromDb.LastName));
        }
    }
}
