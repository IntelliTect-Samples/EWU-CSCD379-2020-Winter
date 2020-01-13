using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
        public void CreateGift_UsingValidInput_Success()
        {
            Gift gift = new Gift(1, "TestTitle", "TestDescription", "TestUrl", new User(1, "TestFirst", "TestLast", new List<Gift>()));

            Assert.AreEqual<int>(1, gift.Id);
            Assert.AreEqual<string>("TestTitle", gift.Title);
            Assert.AreEqual<string>("TestDescription", gift.Description);
            Assert.AreEqual<string>("TestUrl", gift.Url);
            Assert.IsNotNull(gift.User);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateGift_UsingNullTitle_ThrowsArgumentNullException()
        {
            Gift gift = new Gift(1, null!, "TestDescription", "TestUrl", new User(1, "TestFirst", "TestLast", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeGiftTitle_UsingNullTitle_ThrowsArgumentNullException()
        {
            Gift gift = new Gift(1, "TestTitle", "TestDescription", "TestUrl", new User(1, "TestFirst", "TestLast", new List<Gift>()));

            gift.Title = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateGift_UsingNullDescription_ThrowsArgumentNullException()
        {
            Gift gift = new Gift(1, "TestTitle", null!, "TestUrl", new User(1, "TestFirst", "TestLast", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeGiftDescription_UsingNullDescription_ThrowsArgumentNullException()
        {
            Gift gift = new Gift(1, "TestTitle", "TestDescription", "TestUrl", new User(1, "TestFirst", "TestLast", new List<Gift>()));

            gift.Description = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateGift_UsingNullUrl_ThrowsArgumentNullException()
        {
            Gift gift = new Gift(1, "TestTitle", "TestDescription", null!, new User(1, "TestFirst", "TestLast", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeGiftUrl_UsingNullUrl_ThrowsArgumentNullException()
        {
            Gift gift = new Gift(1, "TestTitle", "TestDescription", "TestUrl", new User(1, "TestFirst", "TestLast", new List<Gift>()));

            gift.Url = null!;
        }
    }
}
