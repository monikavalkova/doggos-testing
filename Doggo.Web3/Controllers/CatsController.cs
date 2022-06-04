using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Doggo.API.Models;
using Doggo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class CatsController : ControllerBase
    {
        private ICatsService _service;

        public CatsController(ICatsService service) //todo add to IoC container
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
    }
}