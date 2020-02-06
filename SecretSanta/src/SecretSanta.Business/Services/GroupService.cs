using AutoMapper;
using SecretSanta.Data;

namespace SecretSanta.Business.Services
{
    public class GroupService : EntityService<Group>, IGroupService
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        { }
    }
}