using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Business;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GiftService : EntityService<Gift>, IPostService
    {
        public GiftService(ApplicationDbContext applicationDbContext, IMapper mapper) :
            base(applicationDbContext, mapper)
        {
        }

        public override async Task<Gift> FetchByIdAsync(int id) =>
            await ApplicationDbContext.Set<Gift>().Include(nameof(Gift.User)).SingleAsync(item => item.Id == id);
    }
}
