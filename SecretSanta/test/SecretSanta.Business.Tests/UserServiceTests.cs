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

            User user = SampleData.CreateBillyBob();
            User user2 = SampleData.CreateFredFlintstone();

            await service.InsertAsync(user);
            await service.InsertAsync(user2);

            // Act
            using var dbContextFetch = new ApplicationDbContext(Options);
            User userFromDb = await dbContextFetch.Users.SingleAsync(item => item.Id == user.Id);

            const string updatedLastName = "Not Billy Bob at all";
            userFromDb.LastName = updatedLastName;

            // Update user Id using user2 Id
            await service.UpdateAsync(user2.Id!.Value, userFromDb);

            using var dbContextAssert = new ApplicationDbContext(Options);
            userFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == user.Id);
            var userFromDb2 = await dbContextAssert.Users.SingleAsync(item => item.Id == 2);

            // Assert
            Assert.AreEqual((SampleData.Billy, updatedLastName), (userFromDb.FirstName, userFromDb2.LastName));
            Assert.AreEqual((SampleData.Billy, SampleData.Bob), (userFromDb.FirstName, userFromDb.LastName));
        }

        [TestMethod]
        public async Task Delete_SingleUserOnly_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IUserService userService = new UserService(dbContext, Mapper);

            var user = SampleData.CreateBillyBob();
            var user2 = SampleData.CreateFredFlintstone();

            await userService.InsertAsync(user);
            await userService.InsertAsync(user2);

            await dbContext.SaveChangesAsync();

            // Act
            bool deleted = await userService.DeleteAsync(user.Id!.Value);
            using var dbContextAssert = new ApplicationDbContext(Options);
            User userFromDb = await dbContextAssert.Set<User>().SingleOrDefaultAsync(e => e.Id == user.Id);
            User userFromDb2 = await dbContextAssert.Set<User>().SingleOrDefaultAsync(e => e.Id == user2.Id);

            // Assert
            Assert.IsTrue(deleted);
            Assert.IsNull(userFromDb);
            Assert.AreEqual(user2.FirstName, userFromDb2.FirstName);
            Assert.AreEqual(user2.LastName, userFromDb2.LastName);
            Assert.AreEqual(user2.Id, userFromDb2.Id);
        }
    }
}
