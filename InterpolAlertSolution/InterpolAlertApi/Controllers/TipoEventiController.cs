using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpolAlertApi.Dtos;
using InterpolAlertApi.Models;
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
        private IEventoRepository _eventoRepository;

        public TipoEventiController(ITipoEventoRepository tipoEventoRepository, IEventoRepository eventoRepository)
        {
            _tipoEventoRepository = tipoEventoRepository;
            _eventoRepository = eventoRepository;
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
        // GET: api/tipoeventi/tipoEventoId
        [HttpGet("{tipoEventoId}", Name = "GetTipoEvento")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(TipoEventoDto))]
        public IActionResult GetTipoEvento(int tipoEventoId)
        {
            if (!_tipoEventoRepository.TipoEventoExists(tipoEventoId))

                return NotFound();

            var tipoeventi = _tipoEventoRepository.GetTipoEvento(tipoEventoId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tipoEventoDto = new TipoEventoDto()
            {
                TipoEventoId = tipoeventi.TipoEventoId,
                NomeTipoEvento = tipoeventi.NomeTipoEvento
            };
            return Ok(tipoEventoDto);
        }
        //api/tipovittime/eventi/eventoId
        [HttpGet("eventi/{eventoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(EventoDto))]
        public IActionResult GetTipoVittimaOfAnEvent(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
            {
                return NotFound();
            }

            var tipoEvento = _tipoEventoRepository.GetTipoEventoOfAnEvent(eventoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoEventoDto = new TipoEventoDto()
            {
                TipoEventoId = tipoEvento.TipoEventoId,
                NomeTipoEvento = tipoEvento.NomeTipoEvento
            };

            return Ok(tipoEventoDto);
        }
        //api/tipoeventi/tipoEventiId/eventi
        [HttpGet("{tipoEventoId}/eventi")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetEventiFromATipoVittima(int tipoEventoId)
        {
            if (!_tipoEventoRepository.TipoEventoExists(tipoEventoId))
            {
                return NotFound();
            }

            var tipoeventi = _tipoEventoRepository.GetEventiFromATipoEvento(tipoEventoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventiDto = new List<EventoDto>();

            foreach (var evento in tipoeventi)
            {
                eventiDto.Add(new EventoDto
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

            return Ok(eventiDto);
        }
        //api/tipoeventi
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(TipoEvento))]
        public IActionResult CreateTipoEvento([FromBody]TipoEvento tipoEventoToCreate)
        {
            if (tipoEventoToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var tipoEvento = _tipoEventoRepository.GetTipoEventi().Where(t => t.NomeTipoEvento.Trim().ToUpper() == tipoEventoToCreate.NomeTipoEvento.Trim().ToUpper()).FirstOrDefault();

            if (tipoEvento != null)
            {
                ModelState.AddModelError("", $"TipoEvento {tipoEventoToCreate.NomeTipoEvento} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tipoEventoRepository.CreateTipoEvento(tipoEventoToCreate))
            {
                ModelState.AddModelError("", $"Qualcosa è andato storto durante il salvataggio {tipoEventoToCreate.NomeTipoEvento}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTipoEvento", new { tipoeventoId = tipoEventoToCreate.TipoEventoId }, tipoEventoToCreate);

        }
        //api/tipoeventi/tipoEventiId
        [HttpPut("{tipoEventoId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public IActionResult UpdateTipoEventi(int tipoEventoId, [FromBody]TipoEvento tipoEventiToUpdate)
        {
            if (tipoEventiToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (tipoEventoId != tipoEventiToUpdate.TipoEventoId)
                return BadRequest(ModelState);

            if (!_tipoEventoRepository.TipoEventoExists(tipoEventoId))
                return NotFound();

            if (_tipoEventoRepository.IsDuplicateTipoEvento(tipoEventoId, tipoEventiToUpdate.NomeTipoEvento))
            {
                ModelState.AddModelError("", $"TipoEvento {tipoEventiToUpdate.NomeTipoEvento} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tipoEventoRepository.UpdateTipoEvento(tipoEventiToUpdate))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'aggiornamento {tipoEventiToUpdate.NomeTipoEvento}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
        //api/tipoeventi/tipoEventiId
        [HttpDelete("{tipoEventoId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTipoEventi(int tipoEventoId)
        {
            if (!_tipoEventoRepository.TipoEventoExists(tipoEventoId))
                return NotFound();

            var tipoEventiToDelete = _tipoEventoRepository.GetTipoEvento(tipoEventoId);

            if (_tipoEventoRepository.GetEventiFromATipoEvento(tipoEventoId).Count() > 0)
            {
                ModelState.AddModelError("", $"TipoEvento {tipoEventiToDelete.NomeTipoEvento}" + " non può essere eliminato perché viene utilizzato almeno in un evento");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tipoEventoRepository.DeleteTipoEvento(tipoEventiToDelete))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'eliminazione {tipoEventiToDelete.NomeTipoEvento}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}