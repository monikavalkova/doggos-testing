using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Anima.WebAPI.Models;
using Anima.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anima.WebAPI.Controllers
{

    [ApiController]
    [Route("api/animals")]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AFAResponse>> RegisterForAdoption(AFARequest animal)
        {
            var animalDto = _service.Add(animal);
            return CreatedAtAction(nameof(GetOne), new { Id = animalDto.Id }, animalDto);
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
        public async Task<ActionResult<AnimalsResponse>> GetAll()
        {
            var allAnimalsForAdoption = await _service.GetAll();

            return Ok(new AnimalsResponse() { AnimalsForAdoption = allAnimalsForAdoption });
        }

        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AnimalsResponse>> Filter(string species)
        {
            Species enumSpeciesValue;
            if(!Enum.TryParse<Species>(species, true, out enumSpeciesValue)) return BadRequest("Invalid species value.");
            var animalsForAdoption = await _service.Filter(new Filter(){ Species = enumSpeciesValue });
            if (animalsForAdoption.Count() == 0) return NotFound();
            return Ok(new AnimalsResponse() { AnimalsForAdoption = animalsForAdoption});
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AFAResponse>> Update(string id, [FromBody] AFARequest animal)
        {
            var animalForAdoption = await _service.Replace(id, animal);
            if (animalForAdoption == null) return NotFound();
            return Ok(animalForAdoption);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Remove(string id)
        {
            await _service.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePartially(string id, AFAPatchRequest request)
        {
            var updatedInfo = await _service.PartialUpdate(id, request);
            if (updatedInfo == null) return NotFound();
            return Ok(updatedInfo);
        }
    }
}