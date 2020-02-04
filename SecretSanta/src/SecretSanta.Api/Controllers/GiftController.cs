using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private IGiftService GiftService { get; }

        public GiftController(IGiftService giftService)
        {
            GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET: api/Gift
        [HttpGet]
        public async Task<IEnumerable<Gift>> Get()
        {
            List<Gift> Gifts = await GiftService.FetchAllAsync();
            return Gifts;
        }

        // GET: api/Gift/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Gift>> Get(int id)
        {
            if (await GiftService.FetchByIdAsync(id) is Gift Gift)
            {
                return Ok(Gift);
            }
            return NotFound();
        }

        // POST: api/Gift
        [HttpPost]
        public async Task<Gift> Post([FromBody] Gift value)
        {
            return await GiftService.InsertAsync(value);
        }

        // PUT: api/Gift/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Gift>> Put(int id, [FromBody] Gift value)
        {
            if (await GiftService.FetchByIdAsync(id) is Gift)
            {
                return Ok(await GiftService.UpdateAsync(id, value));
            }
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (await GiftService.FetchByIdAsync(id) is Gift)
            {
                return Ok(await GiftService.DeleteAsync(id));
            }
            return NotFound();
        }
    }
}
