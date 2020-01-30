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
    public class GroupController : ControllerBase
    {

        private IGroupService GroupService { get; }

        public GroupController(IGroupService groupService)
        {
            GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // GET: api/Group
        [HttpGet]
        public async Task<IEnumerable<Group>> Get()
        {
            List<Group> groups = await GroupService.FetchAllAsync();
            return groups;
        }

        // GET: api/Group/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Get(int id)
        {
            if(await GroupService.FetchByIdAsync(id) is { } group)
            {
                return Ok(group);
            }
            return NotFound();
        }

        // POST: api/Group
        [HttpPost]
        public void Post([FromBody] Group value)
        {
            //TODO
        }

        // PUT: api/Group/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Group value)
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
