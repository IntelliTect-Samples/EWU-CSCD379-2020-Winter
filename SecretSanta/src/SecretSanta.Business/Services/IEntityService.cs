using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    public interface IEntityService<TDto, TInputDto>
        where TInputDto : class
        where TDto : class, TInputDto
    {
        Task<List<TDto>> FetchAllAsync();
        Task<TDto> FetchByIdAsync(int id);
        Task<TDto> InsertAsync(TInputDto entity);
        Task<TDto?> UpdateAsync(int id, TInputDto entity);
        Task<bool> DeleteAsync(int id);
    }
}