using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Api;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using System.Threading.Tasks;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Constructor_ValidParam_Success()
        {
            //Arrange
            MockUserService mockUserService = new MockUserService();
            //Act
            using UserController userController = new UserController(mockUserService);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullParam_ThrowsException()
        {
            //Arrange
            //Act
            using UserController userController = new UserController(null!);
            //Assert
        }

        private class MockUserService : IUserService//Not implementing anything because I only need to test the constructors
        {
            public Task<bool> DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<List<User>> FetchAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<User> FetchByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<User> InsertAsync(User entity)
            {
                throw new NotImplementedException();
            }

            public Task<User?> UpdateAsync(int id, User entity)
            {
                throw new NotImplementedException();
            }
        }
    }
}
