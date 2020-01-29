using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public interface IEntityService<TEntity>
    {
        Task<List<TEntity>> FetchAllAsync();
        Task<TEntity> FetchByIdAsync(int id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity[]> InsertAsync(params TEntity[] entity);
        Task<TEntity> UpdateAsync(int id, TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
