﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Controllers
{
    public abstract class BaseApiController<TDto, TInputDto> : ControllerBase where TDto : class, TInputDto where TInputDto : class
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
        public async Task<ActionResult<TDto?>> Put(int id, [FromBody] TInputDto value)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            TDto entity = await Service.UpdateAsync(id, value);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (entity is null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<TDto> Post(TDto entity)
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