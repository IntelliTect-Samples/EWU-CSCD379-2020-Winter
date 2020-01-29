﻿using System;
using System.Collections.Generic;
using AutoMapper;
using SecretSanta.Data;
using System.Text;

namespace SecretSanta.Business
{
    public class GroupService : EntityService<Group>, IEntityService<Group>
    {
        public GroupService(ApplicationDbContext applicationDbContext, IMapper mapper) :
            base(applicationDbContext, mapper)
        {

        }

    }
}
