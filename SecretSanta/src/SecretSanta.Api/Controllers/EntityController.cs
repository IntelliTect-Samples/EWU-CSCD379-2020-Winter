using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;

namespace SecretSanta.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController<TEntity> : ControllerBase where TEntity : class
    {

        private IEntityService<TEntity> EntityService { get; }

        public EntityController(IEntityService<TEntity> service)
        {
            EntityService = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: https://localhost/api/TEntity
        [HttpGet]
        public async Task<IEnumerable<TEntity>> Get()
        {
            List<TEntity> entities = await EntityService.FetchAllAsync();
            return entities;
        }

        // GET: api/TEntity/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            if (await EntityService.FetchByIdAsync(id) is { } entity)
            {
                return Ok(entity);
            }

            return NotFound();
        }

        // POST: api/TEntity
        [HttpPost]
        public async Task<TEntity> Post([FromBody] TEntity value)
        {
            return await EntityService.InsertAsync(value);
        }

        // PUT: api/TEntity/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TEntity>> Put(int id, [FromBody] TEntity value)
        {
            if (await EntityService.FetchByIdAsync(id) != null)
            {
                return Ok(await EntityService.UpdateAsync(id, value));
            }

            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (await EntityService.FetchByIdAsync(id) != null)
            {
                return Ok(await EntityService.DeleteAsync(id));
            }

            return NotFound();
        }

    }

}
