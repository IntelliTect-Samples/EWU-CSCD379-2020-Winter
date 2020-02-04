using AutoMapper;
using SecretSanta.Data;

namespace SecretSanta.Business.Services
{
    public class UserService : EntityService<User>, IUserService
    {
        public UserService(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        { }
    }
}

