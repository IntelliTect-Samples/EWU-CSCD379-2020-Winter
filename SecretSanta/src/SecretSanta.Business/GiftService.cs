﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public class GiftService : EntityService<Gift>, IEntityService<Gift>
    {
        public GiftService(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext, mapper)
        {

        }

        public override async Task<Gift> FetchByIdAsync(int id)
        {
            return await ApplicationDbContext.Set<Gift>().Include(nameof(Gift.User)).SingleAsync(item => item.Id == id);
        }
    }
}
