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
        class MockUser : User
        {
            public MockUser(int id, string firstName, string lastName) : 
                base(firstName, lastName)
            {
                base.Id = id;
            }
        }

        class MockGift : Gift
        {
            public MockGift(int id, string title, string description, string url) :
                base(title, description, url, Data.Tests.SampleData.CreateInigoMontoya())
            {
                base.Id = id;
            }
        }

        [TestMethod]
        public void Map_User_SuccessWithNoIdMapped()
        {
            // Arrange
            (User source, User target) = (
                new MockUser(21, "Jerett", "Latimer"), new MockUser(0, "invalid", "invalid"));
            IMapper mapper = AutomapperConfigurationProfile.CreateMapper();

            // Act
            mapper.Map(source, target);

            // Assert
            Assert.AreNotEqual<int?>(source.Id, target.Id);
        }

        [TestMethod]
        public void Map_Gift_SuccessWithNoIdMapped()
        {
            // Arrange
            (Gift source, Gift target) = (
                new MockGift(21, "Ring", "A Doorbell", "www.ring.com"), new MockGift(0, "invalid", "invalid", "invalid"));
            IMapper mapper = AutomapperConfigurationProfile.CreateMapper();

            // Act
            mapper.Map(source, target);

            // Assert
            Assert.AreNotEqual<int?>(source.Id, target.Id);
        }
    }
}
