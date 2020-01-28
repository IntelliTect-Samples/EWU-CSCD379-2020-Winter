using AutoMapper;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

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
