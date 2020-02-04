using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.MockServices
{
    public abstract class MockService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
    {
        protected abstract TEntity MockEntity(TEntity entity, int id);
        private Dictionary<int, TEntity> Items { get; } = new Dictionary<int, TEntity>();
        public Task<bool> DeleteAsync(int id)
        {
            Task<bool> rv = Task.FromResult<bool>(Items.Remove(id));
            return rv;
        }

        public Task<List<TEntity>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out TEntity? entity))
            {
                Task<TEntity> t1 = Task.FromResult<TEntity>(entity);
                return t1;
            }
            Task<TEntity> t2 = Task.FromResult<TEntity>(null!);
            return t2;
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            int id = Items.Count + 1;
            Items[id] = MockEntity(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<TEntity?> UpdateAsync(int id, TEntity entity)
        {
            Items[id] = entity;
            return Task.FromResult<TEntity?>(Items[id]);
        }
    }
}

