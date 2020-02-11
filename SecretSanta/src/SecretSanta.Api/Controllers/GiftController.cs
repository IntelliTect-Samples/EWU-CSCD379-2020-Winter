using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : BaseApiController<Business.Dto.Gift, Business.Dto.GiftInput, Gift>
    {
        public GiftController(IGiftService giftService)
            : base (giftService)
        { }
    }
}