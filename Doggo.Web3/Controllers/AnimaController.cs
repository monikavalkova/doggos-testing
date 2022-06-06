﻿using System;
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
    [Route("api/rescues")]
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
            return CreatedAtAction(nameof(GetOne), new {Id = animalDto.Id}, animalDto);
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AFAResponse>> Update(string id, [FromBody] AFARequest animal)
        {
            var animalForAdoption = await _service.Replace(id, animal);
            if(animalForAdoption == null) return NotFound();
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
            if(updatedInfo == null) return NotFound();
            return Ok(updatedInfo);
        }
    }
}