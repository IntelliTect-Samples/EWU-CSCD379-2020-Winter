using AutoMapper;
using SecretSanta.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public abstract class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : EntityBase
    {
        protected ApplicationDbContext ApplicationDbContext { get; }
        protected IMapper Mapper { get; }

        public EntityService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            ApplicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
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
        public async Task<TEntity[]> InsertAsync(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                ApplicationDbContext.Set<TEntity>().Add(entity);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return entities;
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
