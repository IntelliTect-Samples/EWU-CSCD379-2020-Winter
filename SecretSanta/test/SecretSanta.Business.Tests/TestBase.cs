using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Tests
{
    public class TestBase : SecretSanta.Data.Tests.TestBase
    {
        // Justification: Set by TestInitialize
#nullable disable // CS8618: Non-nullable field is uninitialized. Consider declaring as nullable.
        protected IMapper Mapper { get; private set; }
#nullable enable

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Mapper = AutomapperProfileConfiguration.CreateMapper();
        }
    }
}
