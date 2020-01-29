using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;

namespace SecretSanta.Business
{

    public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
    {

        protected ApplicationDbContext ApplicationDbContext { get; }

        protected IMapper Mapper { get; }

        public EntityService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            ApplicationDbContext =
                applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TEntity entity = await FetchByIdAsync(id);
            if (entity is { })
            {
                ApplicationDbContext.Remove(entity);
                await ApplicationDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<TEntity>> FetchAllAsync() => await ApplicationDbContext.Set<TEntity>().ToListAsync();

        public virtual async Task<TEntity> FetchByIdAsync(int id) => await ApplicationDbContext.FindAsync<TEntity>(id);

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            ApplicationDbContext.Add(entity);
            await ApplicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity[]> InsertAsync(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                await InsertAsync(entity);
            }

            return entities;
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            if (await ApplicationDbContext.FindAsync<TEntity>(id) is { } result)
            {
                Mapper.Map(entity, result);
                await ApplicationDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

    }

}
