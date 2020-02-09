using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    public abstract class BaseApiController<TDto, TInputDto> : ControllerBase
        where TDto : class, TInputDto
        where TInputDto : class
    {
        protected IEntityService<TDto, TInputDto> Service { get; }

        protected BaseApiController(IEntityService<TDto, TInputDto> service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

       [HttpGet]
        public async Task<IEnumerable<TDto>> Get() => await Service.FetchAllAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(int id)
        {
            TDto entity = await Service.FetchByIdAsync(id);
            if (entity is null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TInputDto value)
        {
            TDto tDto = await Service.UpdateAsync(id, value);
            if(tDto is null)
            {
                return NotFound();
            }
            return Ok(tDto);
        }

        [HttpPost]
        public async Task<TDto> Post(TInputDto entity)
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