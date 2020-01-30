using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GiftService : EntityService<Gift>, IGiftService
    {
        public GiftService(ApplicationDbContext context, IMapper mapper) :
            base(context, mapper)
        { }

        public override async Task<Gift> FetchByIdAsync(int id) =>
            await ApplicationDbContext.Set<Gift>().Include(nameof(User)).SingleAsync(gift => gift.Id == id);
    }
}