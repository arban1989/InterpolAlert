using System;
using System.Collections.Generic;
using System.Linq;
using InterpolAlertApi.Dtos;
using InterpolAlertApi.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InterpolAlertApi.Models;

namespace InterpolAlertApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoVittimeController : ControllerBase
    {
        private ITipoVittimaRepository _tipoVittimaRepository;
        private IEventoRepository _eventoRepository;

        public TipoVittimeController(ITipoVittimaRepository tipoVittimaRepository, IEventoRepository eventoRepository)
        {
            _tipoVittimaRepository = tipoVittimaRepository;
            _eventoRepository = eventoRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TipoVittimaDto>))]
        public IActionResult GetTipiVittima()
        {
            var tipovittimes = _tipoVittimaRepository.GetTipiVittima().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tipovittimesDto = new List<TipoVittimaDto>();
            foreach (var tipovittama in tipovittimes)
            {
                tipovittimesDto.Add(new TipoVittimaDto
                {
                    TipoVittimaId = tipovittama.TipoVittimaId,
                    NomeTipoVittima = tipovittama.NomeTipoVittima
                });
            }
            return Ok(tipovittimesDto);
        }

        //api/tipovittime/tipoVittimaId
        [HttpGet("{tipoVittimaId}", Name = "GetTipoVittima")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(TipoVittimaDto))]
        public IActionResult GetTipoVittima(int tipoVittimaId)
        {
            if (!_tipoVittimaRepository.TipoVittimaExists(tipoVittimaId))
                return NotFound();

            var tipoVittima = _tipoVittimaRepository.GetTipoVittima(tipoVittimaId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoVittimaDto = new TipoVittimaDto()
            {
                TipoVittimaId = tipoVittima.TipoVittimaId,
                NomeTipoVittima = tipoVittima.NomeTipoVittima
            };

            return Ok(tipoVittimaDto);
        }

        //api/tipovittime/eventi/eventoId
        [HttpGet("eventi/{eventoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(TipoVittimaDto))]
        public IActionResult GetTipoVittimaOfAnEvent(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
            {
                return NotFound();
            }

            var tipoVittima = _tipoVittimaRepository.GetTipoVittimaOfAnEvent(eventoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoVittimaDto = new TipoVittimaDto()
            {
                TipoVittimaId = tipoVittima.TipoVittimaId,
                NomeTipoVittima = tipoVittima.NomeTipoVittima
            };

            return Ok(tipoVittimaDto);
        }

        //api/tipovittime/tipoVittimaId/eventi
        [HttpGet("{tipoVittimaId}/eventi")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetEventiFromATipoVittima(int tipoVittimaId)
        {
            if (!_tipoVittimaRepository.TipoVittimaExists(tipoVittimaId))
            {
                return NotFound();
            }

            var eventi = _tipoVittimaRepository.GetEventiFromATipoVittima(tipoVittimaId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventiDto = new List<EventoDto>();

            foreach (var evento in eventi)
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

        //api/tipovittime
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(TipoVittima))]
        public IActionResult CreateTipoVittima([FromBody]TipoVittima tipoVittimaToCreate)
        {
            if (tipoVittimaToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var tipoVittima = _tipoVittimaRepository.GetTipiVittima().Where(t => t.NomeTipoVittima.Trim().ToUpper() == tipoVittimaToCreate.NomeTipoVittima.Trim().ToUpper()).FirstOrDefault();

            if (tipoVittima != null)
            {
                ModelState.AddModelError("", $"TipoVittima {tipoVittimaToCreate.NomeTipoVittima} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tipoVittimaRepository.CreateTipoVittima(tipoVittimaToCreate))
            {
                ModelState.AddModelError("", $"Qualcosa è andato storto durante il salvataggio {tipoVittimaToCreate.NomeTipoVittima}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTipoVittima", new { tipoVittimaId = tipoVittimaToCreate.TipoVittimaId }, tipoVittimaToCreate);

        }


        //api/tipoVittime/tipoVittimaId
        [HttpPut("{tipoVittimaId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public IActionResult UpdateTipoVittima(int tipoVittimaId, [FromBody]TipoVittima tipoVittimaToUpdate)
        {
            if (tipoVittimaToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (tipoVittimaId != tipoVittimaToUpdate.TipoVittimaId)
                return BadRequest(ModelState);

            if (!_tipoVittimaRepository.TipoVittimaExists(tipoVittimaId))
                return NotFound();

            if (_tipoVittimaRepository.IsDuplicateTipoVittima(tipoVittimaId, tipoVittimaToUpdate.NomeTipoVittima))
            {
                ModelState.AddModelError("", $"TipoVitttima {tipoVittimaToUpdate.NomeTipoVittima} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tipoVittimaRepository.UpdateTipoVittima(tipoVittimaToUpdate))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'aggiornamento {tipoVittimaToUpdate.NomeTipoVittima}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        //api/tipoVittime/tipoVittimaId
        [HttpDelete("{tipoVittimaId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTipoVittima(int tipoVittimaId)
        {
            if (!_tipoVittimaRepository.TipoVittimaExists(tipoVittimaId))
                return NotFound();

            var tipoVittimaToDelete = _tipoVittimaRepository.GetTipoVittima(tipoVittimaId);

            if (_tipoVittimaRepository.GetEventiFromATipoVittima(tipoVittimaId).Count() > 0)
            {
                ModelState.AddModelError("", $"TipoVittima {tipoVittimaToDelete.NomeTipoVittima}" + " non può essere eliminato perché viene utilizzato almeno in un evento");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_tipoVittimaRepository.DeleteTipoVittima(tipoVittimaToDelete))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'eliminazione {tipoVittimaToDelete.NomeTipoVittima}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}