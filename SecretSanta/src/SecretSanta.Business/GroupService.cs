using AutoMapper;
using SecretSanta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public class GroupService : EntityService<Group>, IGroupService
    {
        public GroupService(ApplicationDbContext context, IMapper mapper) :
            base(context, mapper)
        { }
    }
}
