﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business.Services
{
    public class UserService : EntityService<Dto.User, Dto.UserInput, User>, IUserService
    {
        public UserService(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        { }

        protected override IQueryable<User> Query
            => base.Query.Include(x => x.Gifts).Include(x => x.UserGroups);
    }
}

