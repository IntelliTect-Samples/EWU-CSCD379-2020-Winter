using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;

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