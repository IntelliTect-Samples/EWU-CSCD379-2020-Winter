using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests
{
    internal abstract class MockEntityService<TEntity> : IEntityService<TEntity>
        where TEntity : EntityBase
    {
        private IDictionary<int, TEntity> Data { get; } = new Dictionary<int, TEntity>();

        protected abstract TEntity AddId(TEntity entity, int id);

        public Task<bool> DeleteAsync(int id) =>
            Task.FromResult(Data.Remove(id));

        public Task<List<TEntity>> FetchAllAsync() =>
            Task.FromResult(Data.Values.ToList());

        public Task<TEntity> FetchByIdAsync(int id) =>
            Task.FromResult(Data.TryGetValue(id, out var entity) ? entity : null!);

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            TEntity add = AddId(entity, Data.Count);
            Data[add.Id] = add;
            return Task.FromResult(add);
        }

        public Task<TEntity?> UpdateAsync(int id, TEntity entity) =>
            Task.FromResult(Data.ContainsKey(id) ? Data[id] = entity : null);
    }
}

