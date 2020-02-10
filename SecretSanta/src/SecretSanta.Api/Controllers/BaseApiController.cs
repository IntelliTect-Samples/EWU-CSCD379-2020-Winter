﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SecretSanta.Api.Controllers
{
    public abstract class BaseApiController<TInputDto, TDto> : ControllerBase 
        where TInputDto : class 
        where TDto : class, TInputDto
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
            return entity is null ? NotFound() : (IActionResult)Ok(entity);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TDto?>> Put(int id, [FromBody] TInputDto value)
        {

            if (await Service.UpdateAsync(id, value) is TDto dto)
            {
                return Ok(dto);
            }
            return NotFound();
           
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