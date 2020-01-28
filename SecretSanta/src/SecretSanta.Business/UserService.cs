using AutoMapper;
using BlogEngine.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Business
{
    public class AuthorService : EntityService<Author>, IAuthorService
    {
        public AuthorService(ApplicationDbContext context, IMapper mapper):
            base(context, mapper)
        { }
    }
}
