using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Api;
using SecretSanta.Business;
using System.Threading.Tasks;
using SecretSanta.Data;
using SecretSanta.Api.Controllers;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        public void Constructor_ValidParam_Success()
        {
            //Arrange
            MockGiftService giftService = new MockGiftService();
            //Act
            using GiftController giftController = new GiftController(giftService);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullParam_ThrowsException()
        {
            //Arrange
            //Act
            using GiftController giftController = new GiftController(null!);
            //Assert
        }

        private class MockGiftService : IGiftService //Only testing constructor so implenting mock class is not neccesary
        {
            public Task<bool> DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<Gift>> FetchAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Gift> FetchByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<Gift> InsertAsync(Gift entity)
            {
                throw new NotImplementedException();
            }

            public Task<Gift?> UpdateAsync(int id, Gift entity)
            {
                throw new NotImplementedException();
            }
        }
    }
}
