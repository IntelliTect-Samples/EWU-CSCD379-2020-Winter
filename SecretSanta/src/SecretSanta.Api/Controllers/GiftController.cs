using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    public class GiftController : BaseApiController<Gift, GiftInput>
    {
        public GiftController(IGiftService giftService)
            : base(giftService)
        { }

        [HttpGet("search/{searchTerm}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Gift>> Search(string searchTerm)
        //public async Task<IActionResult> Get(int id)
        {
            Gift entity = await ((IGiftService)Service).FetchBySearchTermAsync(searchTerm);
            if (entity is null)
            {
                return NotFound();
            }
            List<Gift> entities = new List<Gift>();
            return Ok(entity);
        }
    }
}