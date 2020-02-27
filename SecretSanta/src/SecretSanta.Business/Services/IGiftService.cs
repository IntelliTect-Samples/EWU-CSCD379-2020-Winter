using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    public interface IGiftService : IEntityService<Dto.Gift, Dto.GiftInput>
    {
        public Task<Dto.Gift> FetchBySearchTermAsync(string searchTerm);
    }
}
