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
    public class EsitiController : Controller
    {
        private IEsitoRepository _esitoRepository;
        private IEventoRepository _eventoRepository;

        public EsitiController(IEsitoRepository esitoRepository, IEventoRepository eventoRepository)
        {
            _esitoRepository = esitoRepository;
            _eventoRepository = eventoRepository;
        }


        //api/esiti
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
                    EsitoId = esito.EsitoId,
                    NomeEsito = esito.NomeEsito
                });
            }
            return Ok(esitosDto);
        }


        //api/esiti/esitoId
        [HttpGet("{esitoId}", Name = "GetEsito")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(EsitoDto))]
        public IActionResult GetEsito(int esitoId)
        {
            if (!_esitoRepository.EsitoExists(esitoId))
                return NotFound();

            var esito = _esitoRepository.GetEsito(esitoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var esitoDto = new EsitoDto()
            {
                EsitoId = esito.EsitoId,
                NomeEsito = esito.NomeEsito
            };

            return Ok(esitoDto);
        }

        //api/esiti/eventi/eventoId
        [HttpGet("eventi/{eventoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(LocalitaDto))]
        public IActionResult GetEsitoOfAnEvent(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
            {
                return NotFound();
            }

            var esito = _esitoRepository.GetEsitoOfAnEvent(eventoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var esitoDto = new EsitoDto()
            {
                EsitoId = esito.EsitoId,
                NomeEsito = esito.NomeEsito
            };

            return Ok(esitoDto);
        }

        //api/esiti/esitoId/eventi
        [HttpGet("{esitoId}/eventi")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetEventiFromAnEsito(int esitoId)
        {
            if (!_esitoRepository.EsitoExists(esitoId))
            {
                return NotFound();
            }

            var eventi = _esitoRepository.GetEventiFromAnEsito(esitoId);

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

        //api/esiti
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(Esito))]
        public IActionResult CreateEsito([FromBody]Esito esitoToCreate)
        {
            if (esitoToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var esito = _esitoRepository.GetEsiti().Where(e => e.NomeEsito.Trim().ToUpper() == esitoToCreate.NomeEsito.Trim().ToUpper()).FirstOrDefault();

            if (esito != null)
            {
                ModelState.AddModelError("", $"Esito {esitoToCreate.NomeEsito} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_esitoRepository.CreateEsito(esitoToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {esitoToCreate.NomeEsito}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetEsito", new { esitoId = esitoToCreate.EsitoId }, esitoToCreate);

        }

        //api/esiti
        [HttpPut("{esitoId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public IActionResult UpdateEsito(int esitoId, [FromBody]Esito esitoToUpdate)
        {
            if (esitoToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (esitoId != esitoToUpdate.EsitoId)
                return BadRequest(ModelState);

            if (!_esitoRepository.EsitoExists(esitoId))
                return NotFound();

            if (_esitoRepository.IsDuplicateEsito(esitoId, esitoToUpdate.NomeEsito))
            {
                ModelState.AddModelError("", $"Esito {esitoToUpdate.NomeEsito} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_esitoRepository.UpdateEsito(esitoToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong updating {esitoToUpdate.NomeEsito}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        //api/esiti/esitoId
        [HttpDelete("{esitoId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteEsito(int esitoId)
        {
            if (!_esitoRepository.EsitoExists(esitoId))
                return NotFound();

            var esitoToDelete = _esitoRepository.GetEsito(esitoId);

            if (_esitoRepository.GetEventiFromAnEsito(esitoId).Count() > 0)
            {
                ModelState.AddModelError("", $"Esito {esitoToDelete.NomeEsito}" + " cannot be deletet becouse it is used at least at one event");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_esitoRepository.DeleteEsito(esitoToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {esitoToDelete.NomeEsito}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }

}