using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public interface IEntityService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> FetchAllAsync();
        Task<TEntity> FetchByIdAsync(int id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity[]> InsertAsync(params TEntity[] entities);
        Task<TEntity?> UpdateAsync(int id, TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
