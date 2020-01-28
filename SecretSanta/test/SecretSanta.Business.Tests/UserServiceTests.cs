using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTests : TestBase
    {
        [TestMethod]
        public async Task FetchAllAsync_TwoUsers_Success()
        {
            //Arrange
            User user1 = SampleData.CreateMrKrabs;
            User user2 = SampleData.CreateSpongebob;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            await dbContext.SaveChangesAsync();
            UserService UserService = new UserService(dbContext, _Mapper);
            //Act
            List<User> users = UserService.FetchAllAsync().Result;
            //Assert
            Assert.AreEqual<string>(user1.FirstName, users.ElementAt(0).FirstName);
        }

        [TestMethod]
        public async Task FetchByIdAsync_OneUser_Success()
        {
            //Arrange
            User User = SampleData.CreateMrKrabs;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Users.Add(User);
            await dbContext.SaveChangesAsync();
            UserService UserService = new UserService(dbContext, _Mapper);
            //Act
            User result = UserService.FetchByIdAsync(1).Result;
            //Assert
            Assert.AreEqual<string>(User.FirstName, result.FirstName);
        }

        [TestMethod]
        public async Task InsertAsync_OneUser_Success()
        {
            //Arrange
            User User = SampleData.CreateMrKrabs;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            UserService UserService = new UserService(dbContext, _Mapper);
            //Act
            await UserService.InsertAsync(User);
            //Assert
            User result = await dbContext.Users.FirstOrDefaultAsync();
            Assert.AreEqual<string>(User.FirstName, result.FirstName);
        }

        [TestMethod]
        public async Task UpdateAsync_OneUser_Success()
        {
            //Arrange
            User user1 = SampleData.CreateMrKrabs;
            User user2 = SampleData.CreateSpongebob;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);//have to add user2 here so its fingerprint properties get set, otherwise Sqlite rejects update becasue fingerpint properties are null
            await dbContext.SaveChangesAsync();
            UserService UserService = new UserService(dbContext, _Mapper);
            //Act
            await UserService.UpdateAsync(1, user2);
            //Assert
            User result = await dbContext.Users.FirstOrDefaultAsync();
            Assert.AreEqual(user2.FirstName, result.FirstName);
        }
    }
}
