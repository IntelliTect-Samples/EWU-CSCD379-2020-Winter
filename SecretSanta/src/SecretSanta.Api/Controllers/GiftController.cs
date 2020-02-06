using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : BaseApiController<Business.Dto.Gift, Business.Dto.GiftInput>
    {
        public GiftController(IGiftService giftService)
            : base (giftService)
        { }
    }
}