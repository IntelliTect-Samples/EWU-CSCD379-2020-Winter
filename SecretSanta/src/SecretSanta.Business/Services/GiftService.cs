using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    public class GiftService : EntityService<Dto.Gift, Dto.GiftInput, Data.Gift>, IGiftService
    {
        public GiftService(ApplicationDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        { }

        public async Task<IEnumerable<Dto.Gift>> FetchBySearchTermAsync(string searchTerm)
        {
            //return Mapper.Map<Data.Gift, Dto.Gift>(await Query.FirstOrDefaultAsync(x => x.Title == searchTerm));
            IEnumerable<Gift> gifts = await Query.ToListAsync();
            IEnumerable<Gift> i = gifts.Where(t => t.Title.Contains(searchTerm));
            return Mapper.Map<IEnumerable<Data.Gift>, IEnumerable<Dto.Gift>>(i);
            
        }
    }
}
