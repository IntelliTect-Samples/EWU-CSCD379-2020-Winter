using AutoMapper;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class GroupService : EntityService<Group>, IGroupService
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
