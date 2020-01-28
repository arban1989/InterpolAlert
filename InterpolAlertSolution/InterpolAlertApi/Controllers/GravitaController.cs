using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpolAlertApi.Dtos;
using InterpolAlertApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlertApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GravitaController : ControllerBase
    {
        private IGravitaRepository _gravitaRepository;

        public GravitaController(IGravitaRepository gravitaRepository)
        {
            _gravitaRepository = gravitaRepository;
        }
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GravitaDto>))]

        public IActionResult GetGravities()
        {
            var gravities = _gravitaRepository.GetGravitas().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var gravitiesDto = new List<GravitaDto>();
            foreach (var gravita in gravities)
            {
                gravitiesDto.Add(new GravitaDto
                {
                    IdGravita = gravita.IdGravita,
                    NomeGravita = gravita.NomeGravita
                });
            }
            return Ok(gravitiesDto);
        }
        // GET: api/gravita/1
        [HttpGet("{gravitaId}")]
        public IActionResult GetAutor(int gravitaId)
        {
            var gravita = _gravitaRepository.GetGravita(gravitaId);
            if (gravita == null)
            {
                return NotFound();
            }
            return Ok(gravita);
        }
    }
}