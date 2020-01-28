using AutoMapper;
using BlogEngine.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Business
{
    public class PostService : EntityService<Post>, IPostService
    {
        public PostService(ApplicationDbContext applicationDbContext, IMapper mapper) :
            base(applicationDbContext, mapper)
        {
        }

        public override async Task<Post> FetchByIdAsync(int id) =>
            await ApplicationDbContext.Set<Post>().Include(nameof(Post.Author)).SingleAsync(item => item.Id == id);
    }
}
