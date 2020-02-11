using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : BaseApiController<Gift, GiftInput>
    {
        public GiftController(IGiftService giftService)
            : base (giftService)
        { }
    }
}