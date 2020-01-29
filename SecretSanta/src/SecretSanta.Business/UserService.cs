using AutoMapper;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class UserService : EntityService<User>, IEntityService<User>
    {
        public UserService(ApplicationDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) { }
    }
}