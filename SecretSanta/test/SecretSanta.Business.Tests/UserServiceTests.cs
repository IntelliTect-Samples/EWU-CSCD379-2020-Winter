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
        public async Task InsertAsync_GaiusBaltar_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IUserService service = new UserService(dbContextInsert, Mapper);

            var gaius = SampleData.CreateGaiusBaltar();

            // Act
            await service.InsertAsync(gaius);


            // Assert
            Assert.IsNotNull(gaius.Id);
        }

        [TestMethod]
        public async Task UpdateUser_SaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IUserService service = new UserService(dbContextInsert, Mapper);

            var lee = SampleData.CreateLeeAdama();
            var kara = SampleData.CreateKaraThrace();

            lee = await service.InsertAsync(lee);
            kara = await service.InsertAsync(kara);

            // Act

            const string diffLastName = "Smith";
            lee.LastName = diffLastName;
            kara.LastName = diffLastName;

            // Update Inigo Montoya using the princesses Id.
            await service.UpdateAsync(lee.Id, lee);
            await service.UpdateAsync(kara.Id, kara);

            using var dbContextFetch = new ApplicationDbContext(Options);
            User leeFromDb = await dbContextFetch.Users.SingleAsync(item => item.Id == lee.Id);
            User karaFromDb = await dbContextFetch.Users.SingleAsync(item => item.Id == lee.Id);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);

            leeFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == lee.Id);
            karaFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == lee.Id);

            Assert.AreEqual(leeFromDb.LastName, karaFromDb.LastName);
        }
    }
}

