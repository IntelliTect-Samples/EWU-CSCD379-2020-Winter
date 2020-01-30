using AutoMapper;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GroupService : EntityService<Group>, IGroupService
    {
        public GroupService(ApplicationDbContext context, IMapper mapper) :
            base(context, mapper)
        { }
    }
}