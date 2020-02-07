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
    public class MandantiController : ControllerBase
    {
        private IMandanteRepository _mandanteRepository;
        private IEventoRepository _eventoRepository;
        private IFazioneRepository _fazioneRepository;

        public MandantiController(IMandanteRepository mandanteRepository, IEventoRepository eventoRepository, IFazioneRepository fazioneRepository)
        {
            _mandanteRepository = mandanteRepository;
            _eventoRepository = eventoRepository;
            _fazioneRepository = fazioneRepository;
        }

        //api/mandanti
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MandanteDto>))]

        public IActionResult GetMandanti()
        {
            var mandanti = _mandanteRepository.GetMandanti().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mandantiDto = new List<MandanteDto>();
            foreach (var mandante in mandanti)
            {
                mandantiDto.Add(new MandanteDto
                {
                    MandanteId = mandante.MandanteId,
                    NomeMandante = mandante.NomeMandante
                }
                 ) ;
            }
            return Ok(mandantiDto);
        }


        //api/mandanti/mandanteId
        [HttpGet("{mandanteId}", Name = "GetMandante")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(MandanteDto))]
        public IActionResult GetMandante(int mandanteId)
        {
            if (!_mandanteRepository.MandanteExists(mandanteId))
                return NotFound();

            var mandante = _mandanteRepository.GetMandante(mandanteId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mandanteDto = new MandanteDto()
            {
                MandanteId = mandante.MandanteId,
                NomeMandante = mandante.NomeMandante
            };

            return Ok(mandanteDto);
        }

        //api/mandanti/eventi/eventoId
        [HttpGet("eventi/{eventoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(MandanteDto))]
        public IActionResult GetMandanteOfAnEvent(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
            {
                return NotFound();
            }

            var mandante = _mandanteRepository.GetMandanteOfAnEvent(eventoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mandanteDto = new MandanteDto()
            {
                MandanteId = mandante.MandanteId,
                NomeMandante = mandante.NomeMandante
            };

            return Ok(mandanteDto);
        }

        //api/mandanti/mandanteId/eventi
        [HttpGet("{mandanteId}/eventi")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetEventiFromAMandante(int mandanteId)
        {
            if (!_mandanteRepository.MandanteExists(mandanteId))
            {
                return NotFound();
            }

            var eventi = _mandanteRepository.GetEventiFromAMandante(mandanteId);

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

        //api/mandanti/mandanteId/fazione
        [HttpGet("{mandanteId}/fazione")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(FazioneDto))]
        public IActionResult GetFazioneOfAMandante(int mandanteId)
        {
            if (!_mandanteRepository.MandanteExists(mandanteId))
            {
                return NotFound();
            }

            var fazione = _mandanteRepository.GetFazioneOfAMandante(mandanteId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fazioneDto = new FazioneDto()
            {
                FazioneId = fazione.FazioneId,
                NomeFazione = fazione.NomeFazione
            };

            return Ok(fazioneDto);
        }

        //api/mandanti
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(Mandante))]
        public IActionResult CreateMandante([FromBody]Mandante mandanteToCreate)
        {
            if (mandanteToCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!_fazioneRepository.FazioneExists(mandanteToCreate.Fazione.FazioneId))
            {
                ModelState.AddModelError("", "La fazione non esiste!");
                return StatusCode(404, ModelState);
            }

            mandanteToCreate.Fazione = _fazioneRepository.GetFazione(mandanteToCreate.Fazione.FazioneId);

            var mandante = _mandanteRepository.GetMandanti().Where(m => m.NomeMandante.Trim().ToUpper() == mandanteToCreate.NomeMandante.Trim().ToUpper()).FirstOrDefault();

            if (mandante != null)
            {
                ModelState.AddModelError("", $"Mandante {mandanteToCreate.NomeMandante} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_mandanteRepository.CreateMandante(mandanteToCreate))
            {
                ModelState.AddModelError("", $"Qualcosa è andato storto durante il salvataggio {mandanteToCreate.NomeMandante}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetMandante", new { mandanteId = mandanteToCreate.MandanteId }, mandanteToCreate);

        }

        //api/mandanti/mandanteId
        [HttpPut("{mandanteId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public IActionResult UpdateMandante(int mandanteId, [FromBody]Mandante mandanteToUpdate)
        {
            if (mandanteToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (mandanteId != mandanteToUpdate.MandanteId)
                return BadRequest(ModelState);

            if (!_mandanteRepository.MandanteExists(mandanteId))
                return NotFound();

            if (_mandanteRepository.IsDuplicateMandante(mandanteId, mandanteToUpdate.NomeMandante))
            {
                ModelState.AddModelError("", $"Mandante {mandanteToUpdate.NomeMandante} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_mandanteRepository.UpdateMandante(mandanteToUpdate))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'aggiornamento {mandanteToUpdate.NomeMandante}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        //api/mandandti/mandanteId
        [HttpDelete("{mandanteId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteMandante(int mandanteId)
        {
            if (!_mandanteRepository.MandanteExists(mandanteId))
                return NotFound();

            var mandanteToDelete = _mandanteRepository.GetMandante(mandanteId);

            if (_mandanteRepository.GetEventiFromAMandante(mandanteId).Count() > 0)
            {
                ModelState.AddModelError("", $"Mandante {mandanteToDelete.NomeMandante}" + " non può essere eliminato perché viene utilizzato almeno in un evento");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_mandanteRepository.DeleteMandante(mandanteToDelete))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'eliminazione {mandanteToDelete.NomeMandante}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}