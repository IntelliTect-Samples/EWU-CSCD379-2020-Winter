using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiftController : EntityController<Gift>
    {
        public GiftController(IGiftService giftService) : base(giftService) { }
    }
}

