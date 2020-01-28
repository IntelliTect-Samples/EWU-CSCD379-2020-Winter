using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SecretSanta.Data;
using SecretSanta.Data.Tests;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class AutomapperProfileConfigurationTests
    {
        [TestMethod]
        public void CreateMapper_AllMaps_ValidConfiguration()
        {
            //Arrange
            //Act
            IMapper mapper = AutomapperProfileConfiguration.CreateMapper();
            var config = mapper.ConfigurationProvider;
            //Assert
            config.AssertConfigurationIsValid();
        }
    }
}
