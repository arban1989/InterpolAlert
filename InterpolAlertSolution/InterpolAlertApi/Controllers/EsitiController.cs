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
    public class EsitiController : Controller
    {
        private IEsitoRepository _esitoRepository;

        public EsitiController(IEsitoRepository esitoRepository)
        {
            _esitoRepository = esitoRepository;
        }

        //api/countries
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EsitoDto>))]
        public IActionResult GetCountries()
        {
            var esitos = _esitoRepository.GetEsiti().ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var esitosDto = new List<EsitoDto>();
            foreach (var esito in esitos)
            {
                esitosDto.Add(new EsitoDto
                {
                    IdEsito = esito.IdEsito,
                    NomeEsito = esito.NomeEsito
                });
            }
            return Ok(esitosDto);
        }
    }
}