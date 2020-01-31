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
    public class FazioniController : Controller
    {
        private IFazioneRepository _fazioneRepository;
        private IAutoreRepository _autoreRepository;

        public FazioniController(IFazioneRepository fazioneRepository, IAutoreRepository autoreRepository)
        {
            _fazioneRepository = fazioneRepository;
            _autoreRepository = autoreRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FazioneDto>))]
        
        public IActionResult GetFazioni()
        {
            var fazioni = _fazioneRepository.GetFazioni().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fazioniDto = new List<FazioneDto>();
            foreach (var fazione in fazioni)
            {
                fazioniDto.Add(new FazioneDto
                {
                    FazioneId = fazione.FazioneId,
                    NomeFazione = fazione.NomeFazione
                });
            }
            return Ok(fazioniDto);
        }


        //api/fazioni/fazioneId
        [HttpGet("{fazioneId}", Name = "GetFazione")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(FazioneDto))]
        public IActionResult GetFazione(int fazioneId)
        {
            if (!_fazioneRepository.FazioneExists(fazioneId))
                return NotFound();

            var fazione = _fazioneRepository.GetFazione(fazioneId);

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

        //api/fazioni/autori/autoreId
        [HttpGet("autori/{autoreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(FazioneDto))]
        public IActionResult GetFazioneByAuthor(int autoreId)
        {
            if (!_autoreRepository.AutoreExists(autoreId))
            {
                return NotFound();
            }

            var fazione = _fazioneRepository.GetFazioneByAuthor(autoreId);

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


        //api/fazioni/fazioneId/autori
        [HttpGet("{fazioneId}/autori")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AutoreDto>))]
        public IActionResult GetAutoriFromAFazione(int fazioneId)
        {
            if (!_fazioneRepository.FazioneExists(fazioneId))
            {
                return NotFound();
            }

            var autori = _fazioneRepository.GetAutoriFromAFazione(fazioneId);

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

        //api/fazioni/fazioneId/mandanti
        [HttpGet("{fazioneId}/mandanti")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MandanteDto>))]
        public IActionResult GetMandantiFromAFazione(int fazioneId)
        {
            if (!_fazioneRepository.FazioneExists(fazioneId))
            {
                return NotFound();
            }

            var mandanti = _fazioneRepository.GetMandantiFromAFazione(fazioneId);

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
                });
            }

            return Ok(mandantiDto);

        }


        //api/fazioni
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(Fazione))]
        public IActionResult CreateFazione([FromBody]Fazione fazioneToCreate)
        {
            if (fazioneToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var fazione = _fazioneRepository.GetFazioni().Where(f => f.NomeFazione.Trim().ToUpper() == fazioneToCreate.NomeFazione.Trim().ToUpper()).FirstOrDefault();

            if (fazione != null)
            {
                ModelState.AddModelError("", $"Fazione {fazioneToCreate.NomeFazione} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_fazioneRepository.CreateFazione(fazioneToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {fazioneToCreate.NomeFazione}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetFazione", new { fazioneId = fazioneToCreate.FazioneId }, fazioneToCreate);

        }


        //api/fazioni/fazioneId
        [HttpPut("{fazioneId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public IActionResult UpdateFazione(int fazioneId, [FromBody]Fazione fazioneToUpdate)
        {
            if (fazioneToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (fazioneId != fazioneToUpdate.FazioneId)
                return BadRequest(ModelState);

            if (!_fazioneRepository.FazioneExists(fazioneId))
                return NotFound();

            if (_fazioneRepository.IsDuplicateFazione(fazioneId, fazioneToUpdate.NomeFazione))
            {
                ModelState.AddModelError("", $"Fazione {fazioneToUpdate.NomeFazione} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_fazioneRepository.UpdateFazione(fazioneToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong updating {fazioneToUpdate.NomeFazione}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        //api/fazioni/fazioneId
        [HttpDelete("{fazioneId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteFazione(int fazioneId)
        {
            if (!_fazioneRepository.FazioneExists(fazioneId))
                return NotFound();

            var fazioneToDelete = _fazioneRepository.GetFazione(fazioneId);

            if (_fazioneRepository.GetAutoriFromAFazione(fazioneId).Count() > 0)
            {
                ModelState.AddModelError("", $"Mandante {fazioneToDelete.NomeFazione}" + " cannot be deletet because it is used at least from an autore");
                return StatusCode(409, ModelState);
            }

            if (_fazioneRepository.GetMandantiFromAFazione(fazioneId).Count() > 0)
            {
                ModelState.AddModelError("", $"Mandante {fazioneToDelete.NomeFazione}" + " cannot be deletet because it is used at least from a Mandante");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_fazioneRepository.DeleteFazione(fazioneToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {fazioneToDelete.NomeFazione}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}