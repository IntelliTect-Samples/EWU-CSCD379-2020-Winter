using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : TestBase
    {
        [TestMethod]
        public async Task CreateGift_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            var user = new User("Billy", "Bob");

            var gift = new Gift("Title", "Description", "www.url.com", user);

            await service.InsertAsync(gift);

            // Act

            // Assert
            Assert.IsNotNull(gift.Id);
            Assert.IsNotNull(user.Id);
            Assert.AreSame(gift.User, user);
            Assert.AreEqual(user.Id, gift.User.Id);
        }
    }
}
