using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class UserService : EntityService<User>, IUserService
    {
        public UserService(ApplicationDbContext context, IMapper mapper):
            base(context, mapper)
        { }
    }
}
