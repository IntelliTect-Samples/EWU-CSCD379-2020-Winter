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
        private static MockEntity _MrKrabs = new MockEntity(1, "MrKrabs");
        private static MockEntity _Spongebob = new MockEntity(2, "Spongebob");
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
        public async Task Get_ByID_Success()
        {
            //Arrange
            MockEntityService mockEntityService = new MockEntityService();
            using EntityController<MockEntity> entityController = new EntityController<MockEntity>(mockEntityService);
            //Act
            ActionResult<MockEntity> actionResult = await entityController.Get(1);
            //Assert
            Assert.IsTrue(actionResult.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Get_NoID_Success()
        {
            //Arrange
            MockEntityService mockEntityService = new MockEntityService();
            using EntityController<MockEntity> entityController = new EntityController<MockEntity>(mockEntityService);
            //Act
            IEnumerable<MockEntity> entities = await entityController.Get();
            //Assert
            Assert.AreEqual<int>(1, entities.Count());
            Assert.AreEqual<string>(entities.ElementAt(0).Data, _MrKrabs.Data);
        }

        [TestMethod]
        public async Task Post_AddSpongebob_Success()
        {
            //Arrange
            MockEntityService mockEntityService = new MockEntityService();
            using EntityController<MockEntity> entityController = new EntityController<MockEntity>(mockEntityService);
            //Act
            MockEntity entity = await entityController.Post(_Spongebob);
            //Assert
            Assert.AreEqual<string>(_Spongebob.Data, entity.Data);
            Assert.AreEqual<int>(_Spongebob.Id, entity.Id);
        }

        [TestMethod]
        public async Task Put_MrKrabsToSpongbob_Success()
        {
            //Arrange
            MockEntityService mockEntityService = new MockEntityService();
            using EntityController<MockEntity> entityController = new EntityController<MockEntity>(mockEntityService);
            //Act
            ActionResult<MockEntity> actionResult = await entityController.Put(1, _Spongebob);
            //Asert
            Assert.IsNotNull(actionResult);
            Assert.IsTrue(actionResult.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Delete_MrKrabs_Success()
        {
            //Arrange
            MockEntityService mockEntityService = new MockEntityService();
            using EntityController<MockEntity> entityController = new EntityController<MockEntity>(mockEntityService);
            //Act
            ActionResult actionResult = await entityController.Delete(1);
            //Assert
            Assert.IsTrue(actionResult is OkObjectResult);
        }



        private class MockEntityService : IEntityService<MockEntity>
        {
            private List<MockEntity> Database { get; } = new List<MockEntity>()
            {
                _MrKrabs
            };

            public Task<bool> DeleteAsync(int id)
            {
                int index = id - 1;
                if(Database.ElementAt(index) is MockEntity entityToBeDeleted)
                {
                    Database.RemoveAt(index);
                    Task<bool> t1 = Task.FromResult<bool>(true);
                    return t1;
                }
                Task<bool> t2 = Task.FromResult<bool>(false);
                return t2;
            }

            public Task<List<MockEntity>> FetchAllAsync()
            {
                return Task.FromResult<List<MockEntity>>(Database);
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
                int currentId = Database.Count + 1;
                MockEntity currentEntity = new MockEntity(currentId, entity.Data);
                Database.Add(currentEntity);
                Task<MockEntity> t = Task.FromResult<MockEntity>(currentEntity);
                return t;
            }

            public Task<MockEntity?> UpdateAsync(int id, MockEntity entity)
            {
                int index = id - 1;
                if(Database.ElementAt(index) is MockEntity entityToBeReplaced)
                {
                    MockEntity newEntity = new MockEntity(id, entity.Data);
                    Database[index] = newEntity;
                    Task<MockEntity?> t1 = Task.FromResult<MockEntity?>(newEntity);
                    return t1;
                }
                Task<MockEntity?> t2 = Task.FromResult<MockEntity?>(null);
                return t2;
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
