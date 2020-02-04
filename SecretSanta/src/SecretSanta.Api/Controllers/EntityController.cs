using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class EntityController<T> : ControllerBase where T : class
    {
        private IEntityService<T> EntityService { get; }

        protected EntityController(IEntityService<T> entityService)
        {
            EntityService = entityService ?? throw new System.ArgumentNullException(nameof(entityService));
        }

        [HttpGet]
        public async Task<IEnumerable<T>> Get()
        {
            List<T> entities = await EntityService.FetchAllAsync();
            return entities;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> Get(int id)
        {
            if (await EntityService.FetchByIdAsync(id) is T entity)
            {
                return Ok(entity);
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> Post([FromBody] T value)
        {
            T? entity = await EntityService.InsertAsync(value);

            if (entity is T e) return Ok(e);

            return NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> PutAsync(int id, [FromBody] T value)
        {
            T? entity = await EntityService.UpdateAsync(id, value);

            if (entity is T e) return Ok(e);

            return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            bool success = await EntityService.DeleteAsync(id);

            if (success) return Ok();

            return NotFound();
        }
    }
}

