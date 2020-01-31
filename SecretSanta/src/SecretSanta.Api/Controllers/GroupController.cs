﻿using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    //https://localhost/api/Gi
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IGroupService GroupService { get; }

        public GroupController(IGroupService groupService)
        {
            GroupService = groupService ?? throw new System.ArgumentNullException(nameof(groupService));
        }

        // GET: https://localhost/api/Group
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
            if (await GroupService.FetchByIdAsync(id) is Group group)
            {
                return Ok(group);
            }
            return NotFound();
        }

        // POST: api/Group
        [HttpPost]
        public async Task<Group> Post([FromBody] Group value)
        {
            return await GroupService.InsertAsync(value);
        }

        // PUT: api/Group/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Group>> Put(int id, [FromBody] Group value)
        {
            if (await GroupService.FetchByIdAsync(id) is Group)
            {
                return Ok(await GroupService.UpdateAsync(id, value));
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
            if (await GroupService.FetchByIdAsync(id) is Group)
            {
                return Ok(await GroupService.DeleteAsync(id));
            }
            return NotFound();
        }
    }
}