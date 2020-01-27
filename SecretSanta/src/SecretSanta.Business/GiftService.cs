using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SecretSanta.Business
{
    public class GiftService : EntityService<Gift>, IGiftService
    {
        public GiftService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
