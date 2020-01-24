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
    public class TipoVittimeController : ControllerBase
    {
        private ITipoVittimaRepository _tipoVittimaRepository;

        public TipoVittimeController(ITipoVittimaRepository tipoVittimaRepository)
        {
            _tipoVittimaRepository = tipoVittimaRepository;
        }
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TipoVittimaDto>))]
        public IActionResult GetVictimType()
        {
            var tipovittimes = _tipoVittimaRepository.GetTipiVittima().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tipovittimesDto = new List<TipoVittimaDto>();
            foreach (var tipovittama in tipovittimes)
            {
                tipovittimesDto.Add(new TipoVittimaDto { 
                IdTipoVittima = tipovittama.IdTipoVittima,
                NomeTipoVittima = tipovittama.NomeTipoVittima
                });
            }
            return Ok(tipovittimesDto);
        }
        // GET: api/tipovittime/1
        [HttpGet("{tipovittimeId}")]
        public IActionResult GetAutor(int tipovittimeId)
        {
            var tipovittime = _tipoVittimaRepository.GetTipoVittima(tipovittimeId);
            if (tipovittime == null)
            {
                return NotFound();
            }
            return Ok(tipovittime);
        }
    }
}