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
    public class EventiController : Controller
    {
        private IEventoRepository _eventoRepository;
        private IAutoreRepository _authorRepository;
        private IEsitoRepository _esitoRepository;
        private IFazioneRepository _fazioneRepository;
        private IGravitaRepository _gravitaRepository;
        private ILocalitaRepository _localitaRepository;
        private IMandanteRepository _mandanteRepository;
        private ITipoEventoRepository _tipoEventoRepository;
        private ITipoVittimaRepository _tipoVittimaRepository;

        public EventiController(IEventoRepository eventoRepository, IAutoreRepository authorRepository, 
            IEsitoRepository esitoRepository, IFazioneRepository fazioneRepository, IGravitaRepository gravitaRepository, 
            ILocalitaRepository localitaRepository, IMandanteRepository mandanteRepository, ITipoEventoRepository tipoEventoRepository, 
            ITipoVittimaRepository tipoVittimaRepository)
        {
            _eventoRepository = eventoRepository;
            _authorRepository = authorRepository;
            _esitoRepository = esitoRepository;
            _fazioneRepository = fazioneRepository;
            _gravitaRepository = gravitaRepository;
            _localitaRepository = localitaRepository;
            _mandanteRepository = mandanteRepository;
            _tipoEventoRepository = tipoEventoRepository;
            _tipoVittimaRepository = tipoVittimaRepository;
        }


        //api/eventi
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventoDto>))]
        public IActionResult GetEventi()
        {
            var eventi = _eventoRepository.GetEventi().ToList();

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

        //api/eventi/eventoId
        [HttpGet("{eventoId}", Name = "GetEvento")]
        [ProducesResponseType(200, Type = typeof(EventoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetEvento(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
                return NotFound();

            var evento = _eventoRepository.GetEvento(eventoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var eventoDto = new EventoDto()
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
            };

            return Ok(eventoDto);
        }

        //api/eventi?listaAutoriId=1&listaAutoriId=2&tipoVittimaId=1&localitaId=1&gravitaId=1&esitoId=1&tipoEventoId=1&mandanteId=1
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Evento))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateEvento([FromQuery] List<int> listaAutoriId, [FromQuery] int tipoVittimaId, [FromQuery] int localitaId, 
            [FromQuery] int gravitaId, [FromQuery] int esitoId, [FromQuery] int tipoEventoId, 
            [FromQuery] int mandanteId, [FromBody] Evento eventoToCreate)
        {

            var statusCode = ValidateEvento(listaAutoriId, tipoVittimaId, localitaId, gravitaId, esitoId, tipoEventoId, mandanteId, eventoToCreate);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (!_eventoRepository.CreateEvento(listaAutoriId, tipoVittimaId,localitaId,gravitaId, esitoId, tipoEventoId,mandanteId, eventoToCreate))
            {
                ModelState.AddModelError("", $"Qualcosa è andato storto durante il salvataggio L'Evento " +
                                            $"{eventoToCreate.NomeEvento}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetEvento", new { eventoId = eventoToCreate.EventoId }, eventoToCreate);
        }

        //api/eventi/7?listaAutoriId=1&listaAutoriId=2&tipoVittimaId=2&localitaId=2&gravitaId=2&esitoId=2&tipoEventoId=2&mandanteId=2
        [HttpPut("{eventoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult UpdateEvento(int eventoId, [FromQuery] List<int> listaAutoriId, [FromQuery] int tipoVittimaId, [FromQuery] int localitaId,
            [FromQuery] int gravitaId, [FromQuery] int esitoId, [FromQuery] int tipoEventoId,
            [FromQuery] int mandanteId, [FromBody] Evento eventoToUpdate)
        {
            var statusCode = ValidateEvento(listaAutoriId, tipoVittimaId, localitaId, gravitaId, esitoId, tipoEventoId, mandanteId, eventoToUpdate);

            if (eventoId != eventoToUpdate.EventoId)
                return BadRequest();

            if (!_eventoRepository.EventoExists(eventoId))
                return NotFound();

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            if (!_eventoRepository.UpdateEvento(listaAutoriId, tipoVittimaId, localitaId, gravitaId, esitoId, tipoEventoId, mandanteId, eventoToUpdate))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'aggiornamento the Evento " +
                                            $"{eventoToUpdate.NomeEvento}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //api/eventi/eventoId
        [HttpDelete("{eventoId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteEvento(int eventoId)
        {
            if (!_eventoRepository.EventoExists(eventoId))
                return NotFound();

            var eventoToDelete = _eventoRepository.GetEvento(eventoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_eventoRepository.DeleteEvento(eventoToDelete))
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante l'eliminazione Evento {eventoToDelete.NomeEvento}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        private StatusCodeResult ValidateEvento(List<int> listaAutoriId, int tipoVittimaId, int localitaId, int gravitaId, int esitoId, int tipoEventoId, int mandanteId, Evento evento)
        {
            if (evento == null || listaAutoriId.Count() <= 0 )
            {
                ModelState.AddModelError("", "Evento mancante o lista Autori");
                return BadRequest();
            }

            //if (_eventoRepository.IsDuplicateEvent(evento.EventoId,evento.NomeEvento))
            //{
            //    ModelState.AddModelError("", "Duplicate Nome Evento");
            //    return StatusCode(422);
            //}

            if (!_tipoVittimaRepository.TipoVittimaExists(tipoVittimaId))
            {
                ModelState.AddModelError("", "Tipo Vittima non trovato");
                return StatusCode(404);
            }

            if (!_localitaRepository.LocalitaExists(localitaId))
            {
                ModelState.AddModelError("", "Localita non trovata");
                return StatusCode(404);
            }

            if (!_gravitaRepository.GravitaExists(gravitaId))
            {
                ModelState.AddModelError("", "Gravita non trovata");
                return StatusCode(404);
            }

            if (!_esitoRepository.EsitoExists(esitoId))
            {
                ModelState.AddModelError("", "Esito non trovato");
                return StatusCode(404);
            }

            if (!_tipoEventoRepository.TipoEventoExists(tipoEventoId))
            {
                ModelState.AddModelError("", "Tipo Evento non trovato");
                return StatusCode(404);
            }

            if (!_mandanteRepository.MandanteExists(mandanteId))
            {
                ModelState.AddModelError("", "Mandante non trovato");
                return StatusCode(404);
            }

            foreach (var id in listaAutoriId)
            {
                if (!_authorRepository.AutoreExists(id))
                {
                    ModelState.AddModelError("", "Autore non trovato");
                    return StatusCode(404);
                }
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Errore critico");
                return BadRequest();
            }

            return NoContent();
        }
    }
}