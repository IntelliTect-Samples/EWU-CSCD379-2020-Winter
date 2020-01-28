using AutoMapper;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class UserService : EntityService<User>, IEntityService<User>
    {
        public UserService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
