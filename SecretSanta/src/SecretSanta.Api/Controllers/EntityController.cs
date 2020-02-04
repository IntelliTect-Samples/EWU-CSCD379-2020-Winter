using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController<TEntity> : ControllerBase where TEntity : class
    {
        private IEntityService<TEntity> EntityService { get; }

        public EntityController(IEntityService<TEntity> entityService)
        {
            EntityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
        }

        // GET: https://localhost/api/Gift
        [HttpGet]
        public async Task<IEnumerable<TEntity>> Get()
        {
            List<TEntity> entity = await EntityService.FetchAllAsync();
            return entity;
        }

        // GET: api/Gift/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            if (await EntityService.FetchByIdAsync(id) is TEntity entity)
            {
                return Ok(entity);
            }

            return NotFound();
        }

        // POST: api/Gift
        [HttpPost]
        public async Task<TEntity> Post([FromBody] TEntity value)
        {
            return await EntityService.InsertAsync(value);
        }

        // PUT: api/Gift/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Put(int id, [FromBody] TEntity value)
        {
            if (await EntityService.FetchByIdAsync(id) is TEntity)
            {
                return Ok(await EntityService.UpdateAsync(id, value));
            }

            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (await EntityService.FetchByIdAsync(id) is TEntity)
            {
                return Ok(await EntityService.DeleteAsync(id));
            }

            return NotFound();
        }
    }
}
