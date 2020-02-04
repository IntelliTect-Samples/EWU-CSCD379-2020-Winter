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

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        public void Create_GroupController_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();

            // Act
            _ = new GroupController(service.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            _ = new GroupController(null!);
        }

        [TestMethod]
        public async Task Post_Group_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            Group redTeam = SampleData.CreateRedTeam;
            service.Setup(g => g.InsertAsync(redTeam)).ReturnsAsync(redTeam);
            GroupController controller = new GroupController(service.Object);

            // Act
            redTeam = await controller.Post(redTeam);

            // Assert
            Assert.IsNotNull(redTeam.Id);
        }

        [TestMethod]
        public async Task Put_Group_Success()
        {
            // Arrange
            var service = new Mock<IGroupService>();
            Group group = SampleData.CreateRedTeam;
            TestGroup testGroup = new TestGroup(group, group.Id);
            service.Setup(g => g.UpdateAsync(group.Id, group)).ReturnsAsync(testGroup);
            service.Setup(g => g.FetchByIdAsync(group.Id)).ReturnsAsync(testGroup);
            var controller = new GroupController(service.Object);

            // Act
            ActionResult<Group> result = await controller.Put(group.Id, group);
            OkObjectResult okResult = (OkObjectResult)result.Result;
            Group gResult = (Group)okResult.Value;

            // Assert
            Assert.IsTrue(result.Result is OkObjectResult);
            Assert.AreEqual(gResult.Id, group.Id);
        }

        [TestMethod]
        public async Task Fetch_Group_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            Group redTeam = SampleData.CreateRedTeam;
            Group blueTeam = SampleData.CreateBlueTeam;
            List<Group> groups = new List<Group>();
            groups.Add(redTeam);
            groups.Add(blueTeam);
            service.Setup(g => g.FetchAllAsync()).ReturnsAsync(groups);
            GroupController controller = new GroupController(service.Object);

            // Act
            List<Group> returnGroups = (List<Group>)await controller.Get();

            // Assert
            Assert.AreEqual(2, returnGroups.Count);
        }

        [TestMethod]
        public async Task Delete_Group_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            Group redTeam = SampleData.CreateRedTeam;
            TestGroup testGroup = new TestGroup(redTeam, redTeam.Id);
            service.Setup(g => g.DeleteAsync(redTeam.Id)).ReturnsAsync(true);
            service.Setup(g => g.FetchByIdAsync(redTeam.Id)).ReturnsAsync(testGroup);
            GroupController controller = new GroupController(service.Object);

            // Act
            ActionResult<bool> result = await controller.Delete(redTeam.Id);
            OkObjectResult okResult = (OkObjectResult)result.Result;
            bool gResult = (bool)okResult.Value;

            // Assert
            Assert.IsTrue(result.Result is OkObjectResult);
            Assert.AreEqual(true, gResult);
        }

        private class TestGroup : Group
        {
            public TestGroup(Group group, int id)
                : base(group.Title)
            {
                Id = id;
            }
        }
    }
}
