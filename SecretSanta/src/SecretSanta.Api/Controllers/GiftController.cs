using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Controllers
{
    public class GiftController : BaseApiController<Gift, GiftInput>
    {
        public GiftController(IGiftService giftService)
            : base(giftService)
        { }
    }
}