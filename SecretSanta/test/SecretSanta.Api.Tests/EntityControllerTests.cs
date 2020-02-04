using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class EntityControllerTests
    {
        [TestMethod]
        public void Constructor_ValidParam_Success()
        {
            //Arrange
            MockEntityService entityService = new MockEntityService();
            //Act
            using EntityController<MockEntity> entityController = new EntityController<MockEntity>(entityService);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullParam_ThrowsException()
        {
            //Arrange
            //Act
            using EntityController<MockEntity> entityController = new EntityController<MockEntity>(null!);
            //Assert
        }

        [TestMethod]
        public async Task FetchByIdAsync_ValidID_Success()
        {
            //Arrange
            MockEntityService mockEntityService = new MockEntityService();
            using EntityController<MockEntity> entityController = new EntityController<MockEntity>(mockEntityService);
            //Act
            ActionResult<MockEntity> actionResult = await entityController.Get(1);
            //Assert
            Assert.IsTrue(actionResult.Result is OkObjectResult);
        }

        private class MockEntityService : IEntityService<MockEntity>
        {
            private List<MockEntity> Database { get; } = new List<MockEntity>()
            {
                new MockEntity(1,"Mr.Krabs")
            };

            public Task<bool> DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<MockEntity>> FetchAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<MockEntity> FetchByIdAsync(int id)
            {
                if(Database.ElementAt(id -1) is MockEntity mockEntity)
                {
                    Task<MockEntity> t1 = Task.FromResult<MockEntity>(mockEntity);
                    return t1;
                }
                Task<MockEntity> t2 = Task.FromResult<MockEntity>(new MockEntity(-1,string.Empty));
                return t2;
            }

            public Task<MockEntity> InsertAsync(MockEntity entity)
            {
                throw new NotImplementedException();
            }

            public Task<MockEntity?> UpdateAsync(int id, MockEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        private class MockEntity : FingerPrintEntityBase
        {
            public string Data { get; }
            public MockEntity(int id, string data)
            {
                Id = id;
                Data = data;
            }

        }
    }
}
