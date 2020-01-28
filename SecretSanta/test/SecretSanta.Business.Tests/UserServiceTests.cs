using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Business.Tests;
using Microsoft.Extensions.Options;
using SecretSanta.Data;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_InigoAndPrincess_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IUserService service = new UserService(dbContextInsert, Mapper);

            var inigo = SampleData.CreateInigoMontoya();

            // Act
            await service.InsertAsync(inigo);

            // Assert
            Assert.IsNotNull(inigo.Id);
        }

        [TestMethod]
        public async Task UpdateAuthor_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IUserService service = new UserService(dbContextInsert, Mapper);

            var inigo = SampleData.CreateInigoMontoya();
            var princess = SampleData.CreatePrincessButtercup();

            await service.InsertAsync(inigo);
            await service.InsertAsync(princess);

            // Act
            using var dbContextFetch = new ApplicationDbContext(Options);
            Author inigoFromDb = await dbContextFetch.Authors.SingleAsync(item => item.Id == inigo.Id);

            const string montoyaThe3rd = "Montoya The 3rd";
            inigoFromDb.LastName = montoyaThe3rd;

            // Update Inigo Montoya using the princesses Id.
            await service.UpdateAsync(princess.Id!.Value, inigoFromDb);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            inigoFromDb = await dbContextAssert.Authors.SingleAsync(item => item.Id == inigo.Id);
            var princessFromDb = await dbContextAssert.Authors.SingleAsync(item => item.Id == 2); 

            Assert.AreEqual(
                (SampleData.Inigo, montoyaThe3rd), (princessFromDb.FirstName, princessFromDb.LastName));

            Assert.AreEqual(
                (SampleData.Inigo, SampleData.Montoya), (inigoFromDb.FirstName, inigoFromDb.LastName));
        }
    }
}
