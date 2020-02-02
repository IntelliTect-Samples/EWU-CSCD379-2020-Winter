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
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FetchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

