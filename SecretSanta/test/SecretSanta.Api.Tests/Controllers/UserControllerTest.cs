using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : EntityControllerTest<User>
    {
        public UserControllerTests() : base(new TestableUserService())
        {

        }
        protected override User CreateInstance()
        {
            return SampleData.CreateInigoMontoya();
        }
        private class TestableUserService : EntityService<User>
        {
            protected override User CreateWithId(User entity, int id)
            {
                return new TestUser(entity, id);
            }
        }
        private class TestUser : User
        {
            public TestUser(User entity, int id)
                : base(entity.FirstName, entity.LastName)
            {
                Id = id;
            }
        }
    }
}
