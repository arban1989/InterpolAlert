using InterpolAlert.Models;
using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.ModelsForView
{
    public class EventiForListViewModel
    {
        public int EventoId { get; set; }
        [Required]
        public string NomeEvento { get; set; }

        [Required]
        public string NoteVarie { get; set; }
        [Required]
        public DateTime DataOraInizio { get; set; }
        [Required]
        public DateTime DataOraFine { get; set; }
        public IEnumerable<AutoreDto> Autori { get; set; }
        public LocalitaDto Localita { get; set; }
        public TipoEventoDto TipoEvento { get; set; }

        //public TipoVittimaDto TipoVittima { get; set; }
        public GravitaDto Gravita { get; set; }
        public EsitoDto Esito { get; set; }
        public MandanteDto Mandante { get; set; }

        public Marker Marker { get; set; }
    }
}
