using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    public abstract class BaseApiController<TEntity> : ControllerBase where TEntity : EntityBase
    {
        protected IEntityService<TEntity> Service { get; }

        protected BaseApiController(IEntityService<TEntity> service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

       [HttpGet]
        public async Task<IEnumerable<TEntity>> Get() => await Service.FetchAllAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(int id)
        {
            TEntity entity = await Service.FetchByIdAsync(id);
            if (entity is null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<TEntity?> Put(int id, [FromBody] TEntity value)
        {
            return await Service.UpdateAsync(id, value);
        }

        [HttpPost]
        public async Task<TEntity> Post(TEntity entity)
        {
            return await Service.InsertAsync(entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            if (await Service.DeleteAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}