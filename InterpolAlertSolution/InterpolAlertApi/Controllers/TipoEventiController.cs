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
    public class TipoEventiController : ControllerBase
    {
        private ITipoEventoRepository _tipoEventoRepository;

        public TipoEventiController(ITipoEventoRepository tipoEventoRepository)
        {
            _tipoEventoRepository = tipoEventoRepository;
        }
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TipoEventoDto>))]
        public IActionResult GetEventType()
        {
            var tipoevents = _tipoEventoRepository.GetTipoEventi().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tipoeventsDto = new List<TipoEventoDto>();
            foreach (var tipoevento in tipoevents)
            {
                tipoeventsDto.Add(new TipoEventoDto
                {
                    TipoEventoId = tipoevento.TipoEventoId,
                    NomeTipoEvento = tipoevento.NomeTipoEvento
                });
            }
            return Ok(tipoeventsDto);
        }
        // GET: api/tipoeventi/1
        [HttpGet("{tipoeventiId}")]
        public IActionResult GetAutor(int tipoeventiId)
        {
            var tipoeventi = _tipoEventoRepository.GetTipoEvento(tipoeventiId);
            if (tipoeventi == null)
            {
                return NotFound();
            }
            return Ok(tipoeventi);
        }
    }
}