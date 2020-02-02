using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    public interface IController<TEntity>
    {
        public Task<IEnumerable<TEntity>> Get();

        public Task<ActionResult<TEntity>> Get(int id);

        [HttpPost]
        public Task<ActionResult<TEntity>> Post([FromBody] TEntity value);

        [HttpPut("{id}")]
        public Task<ActionResult<TEntity>> Put(int id, [FromBody] TEntity value);

        [HttpDelete("{id}")]
        public Task<ActionResult<bool>> Delete(int id);
    }
}
