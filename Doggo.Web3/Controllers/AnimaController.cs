using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Doggo.API.Models;
using Doggo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Controllers
{
    
    [ApiController]
    [Route("api/rescues")] //or rescues? TODO ?
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class AnimaController : ControllerBase
    {
        private IAFAService _service;
        
        public AnimaController(IAFAService service)
        {
            _service = service;
        }

        [HttpGet("/ping")]
        [Route("/ping")]
        public ActionResult<string> Ping()
        {
            return Ok(_service.Ping());
        }

        [HttpPost]
        public Task<ActionResult<MessageDto>> RegisterForAdoption(AFARequest animal)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AFAResponse>> GetOne(string id)
        {
            return Ok(await _service.GetOne(id));
        }
    }
}