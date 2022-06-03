using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doggo.API.Models;
using Doggo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaController : ControllerBase
    {
        private ICatsService _service;
        
        public AnimaController(ICatsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/breeds")]
        [Produces("application/json")]
        public Task<ActionResult<IEnumerable<CatDto>>> FindCats(string breed) //e.g., dober
        {
            
            //await _service.GetCatsOfBreed(breed);
            throw new NotImplementedException();
        }

        [HttpGet("/ping")]
        [Route("/ping")]
        [Produces("application/json")]
        public async Task<ActionResult<object>> Ping()
        {
            return await _service.Ping();
        }
    }
}