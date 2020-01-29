using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class AutomapperProfileConfigurationTests
    {
        private class MockGift : Gift
        {
            public MockGift(int id, string title, string description, string url, User user) 
                : base(title, description, url, user)
            {
                base.Id = id;
            }
        }

        private class MockUser : User
        {
            public MockUser(int id, string firstName, string lastName) 
                : base(firstName, lastName)
            {
                base.Id = id;
            }
        }

        private class MockGroup : Group
        {
            public MockGroup(int id, string title)
                : base(title)
            {
                base.Id = id;
            }
        }

        [TestMethod]
        public void Map_Gift_SuccessWithNoIdMapped()
        {
            User user = new User("TestFirst", "TestLast");
            User user2 = new User("TestFirst2", "TestLast2");
            (Gift source, Gift target) = (
                new MockGift(42, "TestTitle", "TestDesc", "www.Test.com", user), new MockGift(0, "Invalid", "Invalid", "Invalid", user2));
            IMapper mapper = AutomapperProfileConfiguration.CreateMapper();
            mapper.Map(source, target);

            Assert.AreNotEqual<int>(source.Id, target.Id);
            Assert.AreEqual<string>(source.Title, target.Title);
            Assert.AreEqual<string>(source.Description, target.Description);
            Assert.AreEqual<string>(source.Url, target.Url);
            Assert.AreEqual<string>(source.User.FirstName, target.User.FirstName);
        }

        [TestMethod]
        public void Map_User_SuccessWithNoIdMapped()
        {
            User source = new MockUser(42, "TestFirst", "TestLast");
            User target = new MockUser(0, "Invalid", "Invalid");
            IMapper mapper = AutomapperProfileConfiguration.CreateMapper();
            mapper.Map(source, target);

            Assert.AreNotEqual<int>(source.Id, target.Id);
            Assert.AreEqual<string>(source.FirstName, target.FirstName);
            Assert.AreEqual<string>(source.LastName, target.LastName);
        }

        [TestMethod]
        public void Map_Group_SuccessWithNoIdMapped()
        {
            Group source = new MockGroup(42, "TestFirst");
            Group target = new MockGroup(0, "Invalid");
            IMapper mapper = AutomapperProfileConfiguration.CreateMapper();
            mapper.Map(source, target);

            Assert.AreNotEqual<int>(source.Id, target.Id);
            Assert.AreEqual<string>(source.Title, target.Title);
        }
    }
}
