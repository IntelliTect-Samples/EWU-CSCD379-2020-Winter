using SecretSanta.Data;
using static SecretSanta.Data.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AutoMapper;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : GenericEntityServicesTestBase<Group>
    {
        protected override IEntityService<Group> GetService(ApplicationDbContext dbContext, IMapper mapper)
        {
            return new GroupService(dbContext, mapper);
        }

        protected override Group CreateEntity()
        {
            return CreateGroup_Cast();
        }
    }
}