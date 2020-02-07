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
    public class AutoriController : Controller
    {
        private IAutoreRepository _autoriRepository;
        private IEventoRepository _eventoRepository;
        private IFazioneRepository _fazioneRepository;

        public AutoriController(IAutoreRepository autoriRepository, IEventoRepository eventoRepository, IFazioneRepository fazioneRepository)
        {
            _autoriRepository = autoriRepository;
            _eventoRepository = eventoRepository;
            _fazioneRepository = fazioneRepository;
        }


        //api/autori
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AutoreDto>))]
        public IActionResult GetAutori()
        {
            var autori = _autoriRepository.GetAutori().ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autoriDto = new List<AutoreDto>();
            foreach (var autore in autori)
            {
                autoriDto.Add(new AutoreDto
                {
                    AutoreId = autore.AutoreId,
                    NomeAutore = autore.NomeAutore,
                    Pericolosita = autore.Pericolosita,
                    NoteVarie = autore.NoteVarie
                });
            }
            return Ok(autoriDto);
        }

        //api/autori/autoreId
        [HttpGet("{autoreId}", Name = "GetAutore")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(AutoreDto))]
        public IActionResult GetAutore(int autoreId)
        {
            if (!_autoriRepository.AutoreExists(autoreId))
                return NotFound();

            var autore = _autoriRepository.GetAutore(autoreId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autoreDto = new AutoreDto()
            {
                AutoreId = autore.AutoreId,
                NomeAutore = autore.NomeAutore,
                Pericolosita = autore.Pericolosita,
                NoteVarie = autore.NoteVarie,
            };

            return Ok(autoreDto);
        }

        //api/autori/autoreId/fazione
        [HttpGet("{autoreId}/fazione")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(FazioneDto))]
        public IActionResult GetFazioneByAutore(int autoreId)
        {
            if (!_autoriRepository.AutoreExists(autoreId))
            {
                return NotFound();
            }

            var fazione = _autoriRepository.GetFazioneByAutore(autoreId);

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

        //api/autori/autoreId/eventi
        [HttpGet("{autoreId}/eventi")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetAllEventiFromAnAutore(int autoreId)
        {
            if (!_autoriRepository.AutoreExists(autoreId))
            {
                return NotFound();
            }

            var eventi = _autoriRepository.GetAllEventiFromAnAutore(autoreId);

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

        //api/autori/eventi/eventoId
        [HttpGet("eventi/{eventoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AutoreDto>))]
        public IActionResult GetAllAutoriFromAnEvent(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
            {
                return NotFound();
            }

            var autori = _autoriRepository.GetAllAutoriFromAnEvent(eventoId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autoriDto = new List<AutoreDto>();
            foreach (var autore in autori)
            {
                autoriDto.Add(new AutoreDto
                {
                    AutoreId = autore.AutoreId,
                    NomeAutore = autore.NomeAutore,
                    Pericolosita = autore.Pericolosita,
                    NoteVarie = autore.NoteVarie
                });
            }

            return Ok(autoriDto);
        }

        //api/autori
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Autore))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateAutore([FromBody]Autore autoreToCreate)
        {
            if (autoreToCreate == null)
                return BadRequest(ModelState);

            if (!_fazioneRepository.FazioneExists(autoreToCreate.Fazione.FazioneId))
            {
                ModelState.AddModelError("", "Fazione non esiste!");
                return StatusCode(404, ModelState);
            }

            autoreToCreate.Fazione = _fazioneRepository.GetFazione(autoreToCreate.Fazione.FazioneId);

            var autore = _autoriRepository.GetAutori().Where(a => a.NomeAutore.Trim().ToUpper() == autoreToCreate.NomeAutore.Trim().ToUpper()).FirstOrDefault();

            if (autore != null)
            {
                ModelState.AddModelError("", $"Autore {autoreToCreate.NomeAutore} esiste già");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_autoriRepository.CreateAutore(autoreToCreate))
            {
                ModelState.AddModelError("", $"Qualcosa è andato storto salvando l'autore " +
                                            $"{autoreToCreate.NomeAutore}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetAutore", new { autoreId = autoreToCreate.AutoreId }, autoreToCreate);
        }

        //api/autori/autoreId
        [HttpPut("{autoreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateAutore(int autoreId, [FromBody]Autore autoreToUpdate)
        {
            if (autoreToUpdate == null)
                return BadRequest(ModelState);

            if (autoreId != autoreToUpdate.AutoreId)
                return BadRequest(ModelState);

            if (!_autoriRepository.AutoreExists(autoreId))
                ModelState.AddModelError("", "L'autore non esiste!");

            if (!_fazioneRepository.FazioneExists(autoreToUpdate.Fazione.FazioneId))
                ModelState.AddModelError("", "La fazione non esiste!");

            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            autoreToUpdate.Fazione = _fazioneRepository.GetFazione(autoreToUpdate.Fazione.FazioneId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_autoriRepository.UpdateAutore(autoreToUpdate))
            {
                ModelState.AddModelError("", $"Qualcosa è andato storto durante l'aggiornamento dell'autore " +
                                            $"{autoreToUpdate.NomeAutore}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //api/autori/autoreId
        [HttpDelete("{autoreId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteAutore(int autoreId)
        {
            if (!_autoriRepository.AutoreExists(autoreId))
                return NotFound();

            var autoreToDelete = _autoriRepository.GetAutore(autoreId);

            if (_autoriRepository.GetAllEventiFromAnAutore(autoreId).Count() > 0)
            {
                ModelState.AddModelError("", $"L'Autore {autoreToDelete.NomeAutore} " +
                                              "non può essere eliminato perché  è associato ad almeno un evento");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_autoriRepository.DeleteAutore(autoreToDelete))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'eliminazione " +
                                            $"{autoreToDelete.NomeAutore}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}