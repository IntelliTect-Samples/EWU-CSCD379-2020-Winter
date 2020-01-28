using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;

namespace SecretSanta.Business.Tests
{
    public class TestBase : Data.Tests.TestBase
    {
// CS8618: Non-nullable field is uninitialized.
// Justification: Field is set by TestInitialize
#nullable disable
        protected IMapper Mapper { get; private set; }
#nullable enable

        [TestInitialize]
        public override void InitializeTests()
        {
            base.InitializeTests();
            Mapper = AutomapperConfigurationProfile.CreateMapper();
        }
    }
}
