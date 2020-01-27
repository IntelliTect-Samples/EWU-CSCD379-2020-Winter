using AutoMapper;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public class UserService : EntityService<User>
    {
        public UserService(ApplicationDbContext dbContext,IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
