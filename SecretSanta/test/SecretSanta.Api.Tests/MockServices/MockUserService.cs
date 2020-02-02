using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.MockServices
{
    public class MockUserService : MockService<User>, IEntityService<User>, IUserService
    {
        protected override User MockEntity(User entity, int id) => new TestUser(entity, id);
    }

    public class TestUser : User
    {
        public TestUser(User user, int id)
            : base(user==null ? throw new ArgumentNullException(nameof(user)) : user.FirstName, user.LastName)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Id = id;
        }
    }
}
