using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.MockServices
{
    public class MockGroupService : MockService<Group>, IEntityService<Group>, IGroupService
    {
        protected override Group MockEntity(Group entity, int id) => new TestGroup(entity, id);
    }

    public class TestGroup : Group
    {
        public TestGroup(Group group, int id)
            : base(group == null ? throw new ArgumentNullException(nameof(group)) : group.Title)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
            Id = id;
        }
    }
}
