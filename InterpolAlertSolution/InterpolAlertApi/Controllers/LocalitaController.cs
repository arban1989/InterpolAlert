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
    public class LocalitaController : Controller
    {
        private ILocalitaRepository _localitaRepository;

        public LocalitaController(ILocalitaRepository localitaRepository)
        {
            _localitaRepository = localitaRepository;
        }

        //api/countries
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LocalitaDto>))]
        public IActionResult GetCountries()
        {
            var localitas = _localitaRepository.GetLocalitas().ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var localitasDto = new List<LocalitaDto>();
            foreach (var localita in localitas)
            {
                localitasDto.Add(new LocalitaDto
                {
                    IdLocalita = localita.IdLocalita,
                    NomeLocalita = localita.NomeLocalita,
                    Latitudine = localita.Latitudine,
                    Longitudine = localita.Longitudine,
                    Nazione = localita.Nazione,
                    LivelloRischio = localita.LivelloRischio

                });
            }
            return Ok(localitasDto);
        }
    }
}