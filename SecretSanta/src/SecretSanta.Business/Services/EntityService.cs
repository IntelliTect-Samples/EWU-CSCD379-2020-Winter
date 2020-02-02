using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
    {
        protected ApplicationDbContext DbContext { get; }
        
        protected IMapper Mapper { get; }
        
        public EntityService(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TEntity? entity = await FetchByIdAsync(id);
            if (entity is { })
            {
                DbContext.Remove(entity);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<TEntity>> FetchAllAsync() =>
            await DbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> FetchByIdAsync(int id) =>
            await DbContext.FindAsync<TEntity?>(id);

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> UpdateAsync(int id, TEntity entity)
        {
            if (await DbContext.FindAsync<TEntity>(id) is { } result)
            {
                Mapper.Map(entity, result);
                await DbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
