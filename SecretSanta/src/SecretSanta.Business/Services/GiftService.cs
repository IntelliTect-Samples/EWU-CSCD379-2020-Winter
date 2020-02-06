using AutoMapper;
using SecretSanta.Data;

namespace SecretSanta.Business.Services
{
    public class GiftService : EntityService<Dto.Gift, Dto.GiftInput, Data.Gift>, IGiftService
    {
        public GiftService(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        { }
    }
}