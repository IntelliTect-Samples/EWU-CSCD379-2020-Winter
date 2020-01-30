﻿using System;
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
            List<Gift> gifts = await GiftService.FetchAllAsync();
            return gifts;
        }

        // GET: api/Gift/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Gift>> Get(int id)
        {
            if (await GiftService.FetchByIdAsync(id) is { } gift)
            {
                return Ok(gift);
            }
            return NotFound();
        }

        // POST: api/Gift
        [HttpPost]
        public void Post([FromBody] Gift value)
        {
            //TODO
        }

        // PUT: api/Gift/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Gift value)
        {
            //TODO
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //TODO
        }
    }
}
