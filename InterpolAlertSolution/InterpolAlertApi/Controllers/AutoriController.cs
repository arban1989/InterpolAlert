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
    public class AutoriController : Controller
    {
        private IAutoreRepository _autoriRepository;

        public AutoriController(IAutoreRepository autoriRepository)
        {
            _autoriRepository = autoriRepository;
        }

        //api/autores
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AutoreDto>))]
        public IActionResult GetAutores()
        {
            var autores = _autoriRepository.GetAutori().ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autoresDto = new List<AutoreDto>();
            foreach (var autore in autores)
            {
                autoresDto.Add(new AutoreDto
                {
                    AutoreId = autore.AutoreId,
                    NomeAutore = autore.NomeAutore,
                    Pericolosita = autore.Pericolosita,
                });
            }
            return Ok(autoresDto);
        }
        // GET: api/autori/1
        [HttpGet("{autoreId}")]
        public IActionResult GetAutor(int autoreId)
        {
            var autori = _autoriRepository.GetAutore(autoreId);
            if (autori == null)
            {
                return NotFound();
            }
            return Ok(autori);
        }
    }
}