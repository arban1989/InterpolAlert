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
                    MandanteId = mandante.MandanteId,
                    NomeMandante = mandante.NomeMandante
                }
                 ) ;
            }
            return Ok(mandantiesDto);
        }


        //api/mandanti/mandanteId
        [HttpGet("{mandanteId}", Name = "GetMandante")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(MandanteDto))]
        public IActionResult GetMandante(int mandanteId)
        {
            if (!_mandanteRepository.MandanteExists(mandanteId))
                return NotFound();
            var mandante = _mandanteRepository.GetMandante(mandanteId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mandanteDto = new MandanteDto()
            {
                MandanteId = mandante.MandanteId,
                NomeMandante = mandante.NomeMandante
            };

            return Ok(mandanteDto);
        }
    }
}