using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : BaseApiController<Gift>
    {
        public GiftController(IGiftService giftService)
            : base (giftService)
        { }
    }
}