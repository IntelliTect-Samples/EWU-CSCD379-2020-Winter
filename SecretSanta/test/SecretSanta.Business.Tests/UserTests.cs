using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser_UsingValidInput_Success()
        {
            User user = new User(1, "TestFirst", "TestLast");

            Assert.AreEqual<int>(1, user.Id);
            Assert.AreEqual<string>("TestFirst", user.FirstName);
            Assert.AreEqual<string>("TestLast", user.LastName);
            Assert.IsNotNull(user.Gifts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateUser_UsingNullFirstName_ThrowsArgumentNullException()
        {
            User user = new User(1, null!, "TestLast");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeUserFirstName_UsingNullFirstName_ThrowsArgumentNullException()
        {
            User user = new User(1, "TestFirst", "TestLast");

            user.FirstName = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateUser_UsingNullLastName_ThrowsArgumentNullException()
        {
            User user = new User(1, "TestFirst", null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeUserLastName_UsingNullLastName_ThrowsArgumentNullException()
        {
            User user = new User(1, "TestFirst", "TestLast");

            user.LastName = null!;
        }
    }
}
