using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    public class GiftService : EntityService<Dto.Gift, Dto.GiftInput, Data.Gift>, IGiftService
    {
        public GiftService(ApplicationDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        { }

        public async Task<Dto.Gift> FetchBySearchTermAsync(string searchTerm)
        {
            return Mapper.Map<Data.Gift, Dto.Gift>(await Query.FirstOrDefaultAsync(x => x.Title == searchTerm));
        }
    }
}
