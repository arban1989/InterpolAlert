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
    public class EventiController : Controller
    {
        private IEventoRepository _eventoRepository;

        public EventiController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        //api/eventi
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetEventi()
        {
            var eventos = _eventoRepository.GetEventi().ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //da finire da problemi con gli elementi virtual
            var eventosDto = new List<EventoDto>();
            foreach (var evento in eventos)
            {
                eventosDto.Add(new EventoDto
                {
                    EventoId = evento.EventoId,
                    NomeEvento = evento.NomeEvento,
                    DataOraInizio = evento.DataOraInizio,
                    DataOraFine = evento.DataOraFine,
                    NrVittime = evento.NrVittime,
                    NrDecessi = evento.NrDecessi,
                    NrFeriti = evento.NrFeriti,
                    NoteVarie = evento.NoteVarie,
                    Mediatore = evento.Mediatore,
                    FFSpeciali = evento.FFSpeciali,
                    Polizia = evento.Polizia,
                    VigiliDelFuoco = evento.VigiliDelFuoco
                });
            }
            return Ok(eventosDto);
        }
    }
}