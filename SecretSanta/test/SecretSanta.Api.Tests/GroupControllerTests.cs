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
    public class GroupControllerTests
    {
        [TestMethod]
        public void Constructor_ValidParam_Success()
        {
            //Arrange
            MockGroupService groupService = new MockGroupService();
            //Act
            using GroupController groupController = new GroupController(groupService);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullParam_ThrowsException()
        {
            //Arrange
            //Act
            using GroupController groupController = new GroupController(null!);
            //Assert
        }

        private class MockGroupService : IGroupService//Not implementing interface because I only need to test constructor
        {
            public Task<bool> DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<Group>> FetchAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Group> FetchByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<Group> InsertAsync(Group entity)
            {
                throw new NotImplementedException();
            }

            public Task<Group?> UpdateAsync(int id, Group entity)
            {
                throw new NotImplementedException();
            }
        }
    }
}
