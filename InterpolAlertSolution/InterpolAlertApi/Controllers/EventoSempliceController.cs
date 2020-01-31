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
    public class EventoSempliceController : Controller
    {
        private IEventoSempliceRepository _eventoSempliceRepository;

        public EventoSempliceController(IEventoSempliceRepository eventoSempliceRepository)
        {
            _eventoSempliceRepository = eventoSempliceRepository;
        }
        //api/eventosemplice
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoSempliceDto>))]
        public IActionResult GetEventiSemplici()
        {
            var simples = _eventoSempliceRepository.GetEventiSemplici().ToList();
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var simpleventsDto = new List<EventoSempliceDto>();
            foreach (var eventoSimp in simples)
            {
                simpleventsDto.Add(new EventoSempliceDto
                {
                    EventoSempliceId = eventoSimp.EventoSempliceId,
                    EventoSempliceNome = eventoSimp.EventoSempliceNome,
                    EventoSempliceNote = eventoSimp.EventoSempliceNote,
                    EventoSempliceData = eventoSimp.EventoSempliceData
                }) ;
            }
            return Ok(simpleventsDto);
        }
        //api/eventosemplice/eventoSempliceId
        [HttpGet("{eventoSempliceId}", Name = "GetEventoSemplice")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(EventoSempliceDto))]
        public IActionResult GetEventoSemplice(int eventoSempliceId)
        {
            if (!_eventoSempliceRepository.EventoSempliceExists(eventoSempliceId))
                return NotFound();
            var eventoSemplice = _eventoSempliceRepository.GetEventoSemplice(eventoSempliceId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var simpleventsDto = new EventoSempliceDto()
            {
            EventoSempliceId = eventoSemplice.EventoSempliceId,
            EventoSempliceNome = eventoSemplice.EventoSempliceNome,
            EventoSempliceNote = eventoSemplice.EventoSempliceNote,
            EventoSempliceData = eventoSemplice.EventoSempliceData
            };

            return Ok(simpleventsDto);
        }
        //api/eventosemplice
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(402)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(EventoSemplice))]
        public IActionResult CreateEventoSemplice([FromBody]EventoSemplice SimpleEventToCreate)
        {
            if (SimpleEventToCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_eventoSempliceRepository.CreateEventoSemplice(SimpleEventToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {SimpleEventToCreate.EventoSempliceNome}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetEventoSemplice", new { eventoSempliceId = SimpleEventToCreate.EventoSempliceId }, SimpleEventToCreate);
            //return Ok(countryToCreate.Id);
        }
        //api/eventosemplice/eventoSempliceId
        [HttpPut("{eventoSempliceId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        public IActionResult UpdateEventoSemplice(int eventoSempliceId, [FromBody]EventoSemplice SimpleEventToUpdate)
        {
            if (SimpleEventToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (eventoSempliceId != SimpleEventToUpdate.EventoSempliceId)
                return BadRequest(ModelState);

            if (!_eventoSempliceRepository.EventoSempliceExists(eventoSempliceId))
                return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_eventoSempliceRepository.UpdateEventoSemplice(SimpleEventToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong updating {SimpleEventToUpdate.EventoSempliceNome}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        //api/eventosemplice/eventoSempliceId
        [HttpDelete("{eventoSempliceId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteEventoSemplice(int eventoSempliceId)
        {
            if (!_eventoSempliceRepository.EventoSempliceExists(eventoSempliceId))
                return NotFound();
            var simpleventToDelete = _eventoSempliceRepository.GetEventoSemplice(eventoSempliceId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_eventoSempliceRepository.DeleteEventoSemplice(simpleventToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {simpleventToDelete.EventoSempliceNome}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}