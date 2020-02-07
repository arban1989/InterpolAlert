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
    public class GravitaController : ControllerBase
    {
        private IGravitaRepository _gravitaRepository;
        private IEventoRepository _eventoRepository;

        public GravitaController(IGravitaRepository gravitaRepository, IEventoRepository eventoRepository)
        {
            _gravitaRepository = gravitaRepository;
            _eventoRepository = eventoRepository;
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
                    GravitaId = gravita.GravitaId,
                    NomeGravita = gravita.NomeGravita
                });
            }
            return Ok(gravitiesDto);
        }

        //api/gravita/gravitaId
        [HttpGet("{gravitaId}", Name = "GetGravita")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(GravitaDto))]
        public IActionResult GetGravita(int gravitaId)
        {
            if (!_gravitaRepository.GravitaExists(gravitaId))
                return NotFound();

            var gravita = _gravitaRepository.GetGravita(gravitaId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gravitaDto = new GravitaDto()
            {
                GravitaId = gravita.GravitaId,
                NomeGravita = gravita.NomeGravita
            };

            return Ok(gravitaDto);
        }

        //api/gravita/eventi/eventoId
        [HttpGet("eventi/{eventoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(GravitaDto))]
        public IActionResult GetGravitaOfAnEvent(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
            {
                return NotFound();
            }

            var gravita = _gravitaRepository.GetGravitaOfAnEvent(eventoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gravitaDto = new GravitaDto()
            {
                GravitaId = gravita.GravitaId,
                NomeGravita = gravita.NomeGravita
            };

            return Ok(gravitaDto);
        }


        //api/gravita/gravitaId/eventi
        [HttpGet("{gravitaId}/eventi")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetEventiFromAGravita(int gravitaId)
        {
            if (!_gravitaRepository.GravitaExists(gravitaId))
            {
                return NotFound();
            }

            var eventi = _gravitaRepository.GetEventiFromAGravita(gravitaId);

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

        //api/gravita
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(Gravita))]
        public IActionResult CreateGravita([FromBody]Gravita gravitaToCreate)
        {
            if (gravitaToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var gravita = _gravitaRepository.GetGravitas().Where(g => g.NomeGravita.Trim().ToUpper() == gravitaToCreate.NomeGravita.Trim().ToUpper()).FirstOrDefault();

            if (gravita != null)
            {
                ModelState.AddModelError("", $"Gravita {gravitaToCreate.NomeGravita} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_gravitaRepository.CreateGravita(gravitaToCreate))
            {
                ModelState.AddModelError("", $"Qualcosa è andato storto durante il salvataggio {gravitaToCreate.NomeGravita}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetGravita", new { gravitaId = gravitaToCreate.GravitaId }, gravitaToCreate);

        }

        //api/gravita
        [HttpPut("{gravitaId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public IActionResult UpdateGravita(int gravitaId, [FromBody]Gravita gravitaToUpdate)
        {
            if (gravitaToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (gravitaId != gravitaToUpdate.GravitaId)
                return BadRequest(ModelState);

            if (!_gravitaRepository.GravitaExists(gravitaId))
                return NotFound();

            if (_gravitaRepository.IsDuplicateGravita(gravitaId, gravitaToUpdate.NomeGravita))
            {
                ModelState.AddModelError("", $"Gravita {gravitaToUpdate.NomeGravita} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_gravitaRepository.UpdateGravita(gravitaToUpdate))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'aggiornamento {gravitaToUpdate.NomeGravita}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        //api/gravita/gravitaId
        [HttpDelete("{gravitaId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteGravita(int gravitaId)
        {
            if (!_gravitaRepository.GravitaExists(gravitaId))
                return NotFound();

            var gravitaToDelete = _gravitaRepository.GetGravita(gravitaId);

            if (_gravitaRepository.GetEventiFromAGravita(gravitaId).Count() > 0)
            {
                ModelState.AddModelError("", $"Esito {gravitaToDelete.NomeGravita}" + " non può essere eliminato perché viene utilizzato almeno in un evento");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_gravitaRepository.DeleteGravita(gravitaToDelete))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'eliminazione {gravitaToDelete.NomeGravita}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}