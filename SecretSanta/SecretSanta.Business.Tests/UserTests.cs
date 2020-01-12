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
        public void Create_User_Success_4args()
        {
            //arrange
            int id = 0;
            string firstName = "John";
            string lastName = "Smith";
            List<Gift> gifts = new List<Gift>();

            User user = new User(id, firstName, lastName, gifts);

            //act

            //assert
            Assert.AreEqual<int>(id, user.Id);
            Assert.AreEqual<string>(firstName, user.FirstName);
            Assert.AreEqual<string>(lastName, user.LastName);
            Assert.AreEqual<List<Gift>>(gifts, user.Gifts);
        }

        [TestMethod]
        public void Create_User_Success_3args()
        {
            //arrange
            int id = 0;
            string firstName = "John";
            string lastName = "Smith";

            User user = new User(id, firstName, lastName);

            //act

            //assert
            Assert.AreEqual<int>(id, user.Id);
            Assert.AreEqual<string>(firstName, user.FirstName);
            Assert.AreEqual<string>(lastName, user.LastName);
            Assert.IsInstanceOfType(user.Gifts, typeof(List<Gift>));
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