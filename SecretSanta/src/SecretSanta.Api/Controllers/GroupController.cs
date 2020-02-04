using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private IGroupService GroupService { get; }

        public GroupController(IGroupService groupService)
        {
            GroupService = groupService;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Group>> Get()
        {
            List<Group> Groups = await GroupService.FetchAllAsync();
            return Groups;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Get(int id)
        {
            if(await GroupService.FetchByIdAsync(id) is Group group)
            {
                return Ok(group);
            }
            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<Group> Post(Group value)
        {
            return await GroupService.InsertAsync(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Put(int id, Group value)
        {
            if(await GroupService.UpdateAsync(id, value) is Group group)
            {
                return Ok(group);
            }
            return NotFound();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(await GroupService.DeleteAsync(id) == true)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
