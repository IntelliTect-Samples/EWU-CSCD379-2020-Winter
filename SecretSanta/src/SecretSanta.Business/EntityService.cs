using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
    {
        protected ApplicationDbContext ApplicationDbContext { get; }
        protected IMapper Mapper { get; }

        public EntityService(ApplicationDbContext dbContext, IMapper mapper)
        {
            ApplicationDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> FetchAllAsync() => 
            await ApplicationDbContext.Set<TEntity>().ToListAsync();

        virtual public async Task<TEntity> FetchByIdAsync(int id) => 
            await ApplicationDbContext.Set<TEntity>().SingleAsync(item => item.Id == id);

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await InsertAsync(new[] { entity });
            return entity;
        }

        public async Task<TEntity[]> InsertAsync(params TEntity[] entity)
        {
            foreach (TEntity e in entity)
            {
                ApplicationDbContext.Set<TEntity>().Add(e);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            TEntity result = await ApplicationDbContext.Set<TEntity>().SingleAsync(item => item.Id == id);
            Mapper.Map(entity, result);
            await ApplicationDbContext.SaveChangesAsync();
            return result;
        }
    }
}
