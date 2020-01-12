using System;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CA1707 // doesn't apply to test
namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
        public void Create_Gift_Success()
        {
            Gift gift = new Gift(
                id:0,
                title:"gift",
                description:"is a gift!",
                url:"http://127.0.0.1:7",
                user:new User(0, "John", "Smith"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_VerifyTitleNotNullable()
        {
            Gift gift = new Gift(0, null!, "", "", new User(0, "John", "Smith"));
        }
    }
}
#pragma warning restore CA1707