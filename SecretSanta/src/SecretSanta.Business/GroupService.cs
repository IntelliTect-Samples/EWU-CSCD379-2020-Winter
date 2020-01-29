using AutoMapper;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GroupService : EntityService<Group>, IEntityService<Group>
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) { }
    }
}