using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpolAlertApi.Dtos;
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

        public FazioniController(IFazioneRepository fazioneRepository)
        {
            _fazioneRepository = fazioneRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FazioneDto>))]
        
        public IActionResult GetFactions()
        {
            var fazionies = _fazioneRepository.GetFazioni().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fazioniesDto = new List<FazioneDto>();
            foreach (var fazione in fazionies)
            {
                fazioniesDto.Add(new FazioneDto
                {
                    FazioneId = fazione.FazioneId,
                    NomeFazione = fazione.NomeFazione
                });
            }
            return Ok(fazioniesDto);
        }
        // GET: api/fazioni/1
        [HttpGet("{fazioneId}")]
        public IActionResult GetFazione(int fazioneId)
        {
            var fazioni = _fazioneRepository.GetFazione(fazioneId);
            if (fazioni == null)
            {
                return NotFound();
            }
            return Ok(fazioni);
        }
    }
}