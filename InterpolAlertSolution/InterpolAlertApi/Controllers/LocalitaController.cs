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
    public class LocalitaController : Controller
    {
        private ILocalitaRepository _localitaRepository;
        private IEventoRepository _eventoRepository;

        public LocalitaController(ILocalitaRepository localitaRepository, IEventoRepository eventoRepository)
        {
            _localitaRepository = localitaRepository;
            _eventoRepository = eventoRepository;
        }


        //api/localita
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LocalitaDto>))]
        public IActionResult GetLocalitas()
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
                    LocalitaId = localita.LocalitaId,
                    NomeLocalita = localita.NomeLocalita,
                    Latitudine = localita.Latitudine,
                    Longitudine = localita.Longitudine,
                    Nazione = localita.Nazione,
                    LivelloRischio = localita.LivelloRischio

                });
            }
            return Ok(localitasDto);
        }

        //api/localita/localitaId
        [HttpGet("{localitaId}", Name = "GetLocalita")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(LocalitaDto))]
        public IActionResult GetLocalita(int localitaId)
        {
            if (!_localitaRepository.LocalitaExists(localitaId))
                return NotFound();
            var localita = _localitaRepository.GetLocalita(localitaId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var localitaDto = new LocalitaDto()
            {
                LocalitaId = localita.LocalitaId,
                NomeLocalita = localita.NomeLocalita,
                Latitudine = localita.Latitudine,
                Longitudine = localita.Longitudine,
                LivelloRischio = localita.LivelloRischio,
                Nazione = localita.Nazione
            };

            return Ok(localitaDto);
        }

        //api/localita/eventi/eventoId
        [HttpGet("eventi/{eventoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(LocalitaDto))]
        public IActionResult GetLocalitaOfAnEvent(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
            {
                return NotFound();
            }

            var localita = _localitaRepository.GetLocalitaOfAnEvent(eventoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var localitaDto = new LocalitaDto()
            {
                LocalitaId = localita.LocalitaId,
                NomeLocalita = localita.NomeLocalita,
                Latitudine = localita.Latitudine,
                Longitudine = localita.Longitudine,
                LivelloRischio = localita.LivelloRischio,
                Nazione = localita.Nazione
            };

            return Ok(localitaDto);
        }

        //api/localita/localitaId/eventi
        [HttpGet("{localitaId}/eventi")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetEventiFromALocalita(int localitaId)
        {
            if (!_localitaRepository.LocalitaExists(localitaId))
            {
                return NotFound();
            }

            var eventi = _localitaRepository.GetEventiFromALocalita(localitaId);

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

        //api/localita
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(Localita))]
        public IActionResult CreateLocalita([FromBody]Localita localitaToCreate)
        {
            if (localitaToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var localita = _localitaRepository.GetLocalitas().Where(l => l.NomeLocalita.Trim().ToUpper() == localitaToCreate.NomeLocalita.Trim().ToUpper()).FirstOrDefault();

            if (localita != null)
            {
                ModelState.AddModelError("", $"Localita {localitaToCreate.NomeLocalita} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_localitaRepository.CreateLocalita(localitaToCreate))
            {
                ModelState.AddModelError("", $"Qualcosa è andato storto durante il salvataggio {localitaToCreate.NomeLocalita}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetLocalita", new { localitaId = localitaToCreate.LocalitaId }, localitaToCreate);
            //return Ok(countryToCreate.Id);

        }

        //api/localita/localitaId
        [HttpPut("{localitaId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public IActionResult UpdateLocalita(int localitaId, [FromBody]Localita localitaToUpdate)
        {
            if (localitaToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (localitaId != localitaToUpdate.LocalitaId)
                return BadRequest(ModelState);

            if (!_localitaRepository.LocalitaExists(localitaId))
                return NotFound();

            if (_localitaRepository.IsDuplicateLocalita(localitaId, localitaToUpdate.NomeLocalita))
            {
                ModelState.AddModelError("", $"Localita {localitaToUpdate.NomeLocalita} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_localitaRepository.UpdateLocalita(localitaToUpdate))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'aggiornamento {localitaToUpdate.NomeLocalita}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        //api/localita/localitaId
        [HttpDelete("{localitaId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteLocalita(int localitaId)
        {
            if (!_localitaRepository.LocalitaExists(localitaId))
                return NotFound();
            var localitaToDelete = _localitaRepository.GetLocalita(localitaId);

            if (_localitaRepository.GetEventiFromALocalita(localitaId).Count() > 0)
            {
                ModelState.AddModelError("", $"Localita {localitaToDelete.NomeLocalita}" + " non può essere eliminato perché viene utilizzato almeno in un evento");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_localitaRepository.DeleteLocalita(localitaToDelete))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'eliminazione {localitaToDelete.NomeLocalita}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}