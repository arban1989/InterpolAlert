using System;
using System.Collections.Generic;
using System.Linq;
using InterpolAlertApi.Dtos;
using InterpolAlertApi.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlertApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MandantiController : ControllerBase
    {
        private IMandanteRepository _mandanteRepository;

        public MandantiController(IMandanteRepository mandanteRepository)
        {
            _mandanteRepository = mandanteRepository;
        }
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MandanteDto>))]

        public IActionResult GetMandanties()
        {
            var mandanties = _mandanteRepository.GetMandanti().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mandantiesDto = new List<MandanteDto>();
            foreach (var mandante in mandanties)
            {
                mandantiesDto.Add(new MandanteDto
                {
                    IdMandante = mandante.IdMandante,
                    NomeMandante = mandante.NomeMandante
                }
                 ) ;
            }
            return Ok(mandantiesDto);
        }
        // GET: api/mandanti/1
        [HttpGet("{mandantiId}")]
        public IActionResult GetMandante(int mandantiId)
        {
            var mandanti = _mandanteRepository.GetMandante(mandantiId);
            if (mandanti == null)
            {
                return NotFound();
            }
            return Ok(mandanti);
        }
    }
}