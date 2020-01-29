using AutoMapper;
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
