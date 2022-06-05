using System;
using System.Collections.Generic;
using System.Linq;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AFAResponse>> GetOne(string id)
        {
            var afa = await _service.GetOne(id);
            if (afa == null) return NotFound();
            return Ok(afa);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AFAResponse>> GetAll()
        {
            var allAnimalsForAdoption = await _service.GetAll();
            return Ok(allAnimalsForAdoption);
        }

        [HttpPost("limit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AFAResponse>> Filter([FromBody] Filter filter)
        {
            var animalsForAdoption = await _service.Filter(filter);
            if(animalsForAdoption.Count() == 0) return NotFound();
            return Ok(animalsForAdoption);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Remove(string id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}