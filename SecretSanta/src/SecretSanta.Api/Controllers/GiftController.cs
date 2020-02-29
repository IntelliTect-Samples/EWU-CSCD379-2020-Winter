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
        public async Task<ActionResult<IEnumerable<Gift>>> Search(string searchTerm)
        {
            IEnumerable<Gift> entities = await ((IGiftService)Service).FetchBySearchTermAsync(searchTerm);
            if (entities is null)
            {
                return NotFound();
            }
            return Ok(entities);
        }
    }
}