using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Create_UserController_Success()
        {
            var service = new TestUserService();

            _ = new UserController(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            _ = new UserController(null!);
        }

        [TestMethod]
        public async Task GetById_WithExistingUser_Success()
        {
            // Arrange
            var service = new TestUserService();
            User user = SampleData.CreateInigoMontoya();
            user = await service.InsertAsync(user);

            var controller = new UserController(service);

            // Act
            ActionResult<User> rv = await controller.Get(user.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Get_ReturnsListOfUsers_Success()
        {
            // Arrange
            var service = new TestUserService();
            User user = SampleData.CreateInigoMontoya();
            User user2 = SampleData.CreatePrincessButtercup();
            user = await service.InsertAsync(user);
            user2 = await service.InsertAsync(user2);

            var controller = new UserController(service);

            // Act
            IEnumerable<User> rv = await controller.Get();

            // Assert
            Assert.AreEqual<int>(2, rv.Count());
        }

        [TestMethod]
        public async Task GetById_WithExistingUser_IdNotFound()
        {
            // Arrange
            var service = new TestUserService();
            User user = SampleData.CreateInigoMontoya();
            user = await service.InsertAsync(user);

            var controller = new UserController(service);

            // Act
            ActionResult<User> rv = await controller.Get(3);

            // Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Put_UpdatesUser_Success()
        {
            // Arrange
            var service = new TestUserService();
            User user = SampleData.CreateInigoMontoya();
            User user2 = SampleData.CreatePrincessButtercup();
            user = await service.InsertAsync(user);
            user2 = await service.InsertAsync(user2);

            var controller = new UserController(service);

            // Act
            ActionResult<User> rv = await controller.Put(user.Id, user2);

            // Assert
            Assert.AreEqual<string>(SampleData.Buttercup, rv.Value.LastName);
        }

        [TestMethod]
        public async Task Delete_User_Success()
        {
            // Arrange
            var service = new TestUserService();
            User user = SampleData.CreateInigoMontoya();
            user = await service.InsertAsync(user);

            var controller = new UserController(service);

            // Act
            ActionResult<bool> rv = await controller.Delete(user.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Delete_User_IdNotFound()
        {
            // Arrange
            var service = new TestUserService();
            User user = SampleData.CreateInigoMontoya();
            user = await service.InsertAsync(user);

            var controller = new UserController(service);

            // Act
            ActionResult<bool> rv = await controller.Delete(3);

            // Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }
    }

    public class TestUserService : IUserService
    {
        private Dictionary<int, User> Items { get; } = new Dictionary<int, User>();

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(Items.Remove(id));
        }

        public Task<List<User>> FetchAllAsync()
        {
            return Task.FromResult(Items.Values.ToList());
        }

        public Task<User> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out User? user))
            {
                Task<User> task1 = Task.FromResult<User>(user);
                return task1;
            }
            Task<User> task2 = Task.FromResult<User>(null!);
            return task2;
        }

        public Task<User> InsertAsync(User entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestUser(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<User?> UpdateAsync(int id, User entity)
        {
            Items[id] = entity;
            return Task.FromResult<User?>(Items[id]);
        }

        private class TestUser : User
        {
            public TestUser(User user,  int id) 
                : base((user ?? throw new ArgumentNullException(nameof(user))).FirstName, user.LastName)
            {
                Id = id;
            }
        }
    }
}
