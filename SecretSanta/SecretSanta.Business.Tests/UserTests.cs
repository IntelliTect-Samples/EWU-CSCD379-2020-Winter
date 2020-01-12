using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CA1707 // doesn't apply to test
namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Create_User_Success()
        {
            User user = new User(
                id:0,
                firstName:"John",
                lastName:"Smith",
                gifts:new List<Gift>());

            user = new User(
                id: 0,
                firstName: "John",
                lastName: "Smith");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_VerifyFirstNameNotNullable()
        {
            User user = new User(0, null!, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_VerifyLastNameNotNullable()
        {
            User user = new User(0, "", null!);
        }
    }
}
#pragma warning restore CA1707