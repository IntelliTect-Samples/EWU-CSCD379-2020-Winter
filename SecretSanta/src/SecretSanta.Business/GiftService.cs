using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public class GiftService : EntityService<Gift>, IGiftService
    {
        public GiftService(ApplicationDbContext context, IMapper mapper) :
           base(context, mapper)
        { }

        public override async Task<Gift> FetchByIdAsync(int id)
        {
            return await ApplicationDbContext.Gifts.Include(g => g.User)
                .SingleOrDefaultAsync(item => item.Id == id);
        }

        public override async Task<List<Gift>> FetchAllAsync()
        {
            return await ApplicationDbContext.Gifts.Include(gift => gift.User).ToListAsync();
        }
    }
}
