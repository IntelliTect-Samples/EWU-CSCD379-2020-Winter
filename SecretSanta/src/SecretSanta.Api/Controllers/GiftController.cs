using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Services;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    //https://localhost/api/Gift
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase, IController<Gift>
    {
        private IGiftService GiftService { get; }

        public GiftController(IGiftService giftService)
        {
            GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET: https://localhost/api/Gift
        [HttpGet]
        public async Task<IEnumerable<Gift>> Get()
        {
            var gifts = await GiftService.FetchAllAsync();
            return gifts;
        }

        // GET: api/Gift/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Gift>> Get(int id)
        {
            if (await GiftService.FetchByIdAsync(id) is { } gift)
                return Ok(gift);

            return NotFound();
        }

        // POST: api/Gift
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Gift>> Post([FromBody] Gift value)
        {
            return Ok(await GiftService.InsertAsync(value));
        }

        // PUT: api/Gift/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<ActionResult<Gift>> Put(int id, [FromBody] Gift value)
        {
            if (await GiftService.FetchByIdAsync(id) is { } gift)
                return Ok(await GiftService.UpdateAsync(id, gift));

            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (await GiftService.FetchByIdAsync(id) is { } gift)
                return Ok(await GiftService.DeleteAsync(id));

            return NotFound();
        }
    }
}
