using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;

namespace SecretSanta.Business
{

    public class GiftService : EntityService<Gift>, IGiftService
    {

        public GiftService(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext, mapper)
        {
        }

        public override async Task<Gift> FetchByIdAsync(int id) =>
            await ApplicationDbContext.Set<Gift>().Include(nameof(Gift.User)).SingleAsync(gift => gift.Id == id);

    }

}
