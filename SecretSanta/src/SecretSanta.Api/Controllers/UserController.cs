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
    //https://localhost/api/User
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IController<User>
    {
        private IUserService UserService { get; }

        public UserController(IUserService userService)
        {
            UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET: https://localhost/api/User
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var users = await UserService.FetchAllAsync();
            return users;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Get(int id)
        {
            if (await UserService.FetchByIdAsync(id) is { } user)
                return Ok(user);

            return NotFound();
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] User value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
