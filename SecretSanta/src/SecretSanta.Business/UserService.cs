using AutoMapper;
using SecretSanta.Data;

namespace SecretSanta.Business
{

    public class UserService : EntityService<User>, IUserService
    {

        public UserService(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext, mapper)
        {
        }

    }

}
