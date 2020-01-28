using AutoMapper;
using SecretSanta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public class GiftService : EntityService<Gift>, IGiftService
    {
        public GiftService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
        override public async Task<List<Gift>> FetchAllAsync() =>
            await ApplicationDbContext.Set<Gift>().Include(g => g.User).ToListAsync();

        override public async Task<Gift> FetchByIdAsync(int id) =>
            await ApplicationDbContext.Set<Gift>().Include(g => g.User).SingleAsync(item => item.Id == id);
    }
}

