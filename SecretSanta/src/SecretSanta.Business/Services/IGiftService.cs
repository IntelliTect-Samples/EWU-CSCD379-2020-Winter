using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    public interface IGiftService : IEntityService<Dto.Gift, Dto.GiftInput>
    {
        public Task<IEnumerable<Dto.Gift>> FetchBySearchTermAsync(string searchTerm);
    }
}
