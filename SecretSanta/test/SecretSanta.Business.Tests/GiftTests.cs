using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {

        [TestMethod]
        public void Create_Gift_Success()
        {
            // arrange 
            int id = 5;
            string title = "hehe";
            string description = "yoyo";
            string url = "www.joemama.com";
            User user = new User(0, "<fistName>", "<lastName>", new List<Gift>());

            // act 
            Gift gift = new Gift(id, title, description, url, user);

            // assert 
            Assert.AreEqual<int>(id, gift.Id, "Id value is unexpected");
            Assert.AreEqual<string>(title, gift.Title, "title value is unexpected");
            Assert.AreEqual<string>(description, gift.Description, "description value is unexpected");
            Assert.AreEqual<string>(url, gift.Url, "url value is unexpected");
            Assert.AreEqual<User>(user, gift.User, "user value is unexpected");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(nameof(Gift.Title))]
        [DataRow(nameof(Gift.Description))]
        [DataRow(nameof(Gift.Url))]
        [DataRow(nameof(Gift.User))]
        public void Properites_AssignNull_ThrowArgumentNullException(string propertyName)
        {
            SetPropertyOnGift(propertyName, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(nameof(Gift.Title), "")]
        [DataRow(nameof(Gift.Title), " ")]
        [DataRow(nameof(Gift.Title), "\t")]
        [DataRow(nameof(Gift.Description), "")]
        [DataRow(nameof(Gift.Description), " ")]
        [DataRow(nameof(Gift.Description), "\t")]
        [DataRow(nameof(Gift.Url), "")]
        [DataRow(nameof(Gift.Url), " ")]
        [DataRow(nameof(Gift.Url), "\t")]
        public void Properties_NullTypeArgument_ThrowArgumentException(string propertyName, string value)
        {
            SetPropertyOnGift(propertyName, value);
        }

        private static void SetPropertyOnGift(
    string propertyName, string? value)
        {
            Gift gift = new Gift(
                0, "<title>", "<description>", "<url>", new User(0, "<fistName>", "<lastName>", new List<Gift>()));

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
