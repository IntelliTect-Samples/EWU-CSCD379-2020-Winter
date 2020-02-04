using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    //https://localhost/api/Gift
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : EntityController<Gift>
    {
        public GiftController(IGiftService entityService) : base(entityService)
        {

        }
    }
}