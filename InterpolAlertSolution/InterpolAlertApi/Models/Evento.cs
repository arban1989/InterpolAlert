using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }
        [Required]
        public string NomeEvento { get; set; }
        [Required]
        public DateTime DataOraInizio { get; set; }
        [Required]
        public DateTime DataOraFine { get; set; }
        [Required]
        public int NrVittime { get; set; }
        [Required]
        public int NrDecessi { get; set; }
        [Required]
        public int NrFeriti { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "La nota deve contenere tra 20 e 200 caratteri")]
        public string NoteVarie { get; set; }
        public bool Mediatore { get; set; }
        public bool FFSpeciali { get; set; }
        public bool Polizia { get; set; }
        public bool VigiliDelFuoco { get; set; }
        public virtual Esito Esito { get; set; }
        public virtual TipoVittima TipoVittima { get; set; }
        public virtual Localita Localita { get; set; }
        public virtual Gravita Gravita { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }
        public virtual Mandante Mandante { get; set; }
        public virtual ICollection<AutoriEventi> AutoriEventi { get; set; }



    }
}
