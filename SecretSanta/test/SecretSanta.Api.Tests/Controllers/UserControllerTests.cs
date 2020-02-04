using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Collections.Generic;
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
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();

            // Act
            _ = new UserController(service.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            _ = new UserController(null!);
        }

        [TestMethod]
        public async Task Post_User_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            User user = SampleData.CreateInigoMontoya();
            service.Setup(g => g.InsertAsync(user)).ReturnsAsync(user);
            UserController controller = new UserController(service.Object);

            // Act
            user = await controller.Post(user);

            // Assert
            Assert.IsNotNull(user.Id);
        }

        [TestMethod]
        public async Task Put_User_Success()
        {
            // Arrange
            var service = new Mock<IUserService>();
            User user = SampleData.CreateJerettLatimer();
            TestUser testUser = new TestUser(user, user.Id);
            service.Setup(g => g.UpdateAsync(user.Id, user)).ReturnsAsync(testUser);
            service.Setup(g => g.FetchByIdAsync(user.Id)).ReturnsAsync(testUser);
            var controller = new UserController(service.Object);

            // Act
            ActionResult<User> result = await controller.Put(user.Id, user);
            OkObjectResult okResult = (OkObjectResult)result.Result;
            User gResult = (User)okResult.Value;

            // Assert
            Assert.IsTrue(result.Result is OkObjectResult);
            Assert.AreEqual(gResult.Id, user.Id);
        }

        [TestMethod]
        public async Task Fetch_User_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            User jerett = SampleData.CreateJerettLatimer();
            User inigo = SampleData.CreateInigoMontoya();
            List<User> users = new List<User>();
            users.Add(jerett);
            users.Add(inigo);
            service.Setup(g => g.FetchAllAsync()).ReturnsAsync(users);
            UserController controller = new UserController(service.Object);

            // Act
            List<User> returnUsers = (List<User>)await controller.Get();

            // Assert
            Assert.AreEqual(2, returnUsers.Count);
        }

        [TestMethod]
        public async Task Delete_User_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            User user = SampleData.CreateInigoMontoya();
            TestUser testUser = new TestUser(user, user.Id);
            service.Setup(g => g.DeleteAsync(user.Id)).ReturnsAsync(true);
            service.Setup(g => g.FetchByIdAsync(user.Id)).ReturnsAsync(testUser);
            UserController controller = new UserController(service.Object);

            // Act
            ActionResult<bool> result = await controller.Delete(user.Id);
            OkObjectResult okResult = (OkObjectResult)result.Result;
            bool gResult = (bool)okResult.Value;

            // Assert
            Assert.IsTrue(result.Result is OkObjectResult);
            Assert.AreEqual(true, gResult);
        }

        private class TestUser : User
        {
            public TestUser(User user, int id)
                : base(user.FirstName, user.LastName)
            {
                Id = id;
            }
        }
    }
}
