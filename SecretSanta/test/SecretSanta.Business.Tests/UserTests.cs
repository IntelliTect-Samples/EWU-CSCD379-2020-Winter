using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTests
    {

        [TestMethod]
        public void Create_User_Success()
        {
            // arrange 
            int id = 5;
            string firstName = "hehe";
            string lastName = "yoyo";
            List<Gift> gifts = new List<Gift>();

            // act 
            User gift = new User(id, firstName, lastName, gifts);

            // assert 
            Assert.AreEqual<int>(id, gift.Id, "Id value is unexpected");
            Assert.AreEqual<string>(firstName, gift.FirstName, "title value is unexpected");
            Assert.AreEqual<string>(lastName, gift.LastName, "description value is unexpected");
            Assert.AreEqual<List<Gift>>(gifts, gift.Gifts, "url value is unexpected");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(nameof(User.FirstName))]
        [DataRow(nameof(User.LastName))]
        [DataRow(nameof(User.Gifts))]
        public void Properites_AssignNull_ThrowArgumentNullException(string propertyName)
        {
            SetPropertyOnGift(propertyName, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(nameof(User.FirstName), "")]
        [DataRow(nameof(User.FirstName), " ")]
        [DataRow(nameof(User.FirstName), "\t")]
        [DataRow(nameof(User.LastName), "")]
        [DataRow(nameof(User.LastName), " ")]
        [DataRow(nameof(User.LastName), "\t")]
        public void Properties_NullTypeArgument_ThrowArgumentException(string propertyName, string value)
        {
            SetPropertyOnGift(propertyName, value);
        }

        private static void SetPropertyOnGift(
    string propertyName, string? value)
        {
            User gift = new User(
                0, "<fistName>", "<lastName>", new List<Gift>());

            //Retrieve the property information based on the type
            System.Reflection.PropertyInfo propertyInfo
                = gift.GetType().GetProperty(propertyName)!;

            try
            {
                //Set the value of the property
                propertyInfo.SetValue(gift, value, null);
            }
            catch (System.Reflection.TargetInvocationException exception)
            {
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(exception.InnerException!).Throw();
            }
        }
    }
}
