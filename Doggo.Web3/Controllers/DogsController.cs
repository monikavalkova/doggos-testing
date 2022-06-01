using System;
using System.Collections.Generic;
using Doggo.API.Models;
using Doggo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private IDogsService _service;
        
        public DogsController(IDogsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/breeds")]
        [Produces("application/json")]
        public IEnumerable<DogDto> GetDogsByBreed()
        {
            // https://api.thecatapi.com/v1/breeds/search?q=air TODO
            throw new NotImplementedException();
        }
    }
}