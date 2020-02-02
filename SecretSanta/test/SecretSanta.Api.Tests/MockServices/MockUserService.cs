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
    }
}
