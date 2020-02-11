using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business.Services
{
    public class GroupService : EntityService<Dto.Group, Dto.GroupInput, Group>, IGroupService
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        { }

        protected override IQueryable<Group> Query => base.Query.Include(x => x.UserGroups);
    }
}
