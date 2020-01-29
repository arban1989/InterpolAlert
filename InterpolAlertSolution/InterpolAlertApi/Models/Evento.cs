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
        public DateTime DataOraFine { get; set; }
        public int NrVittime { get; set; }
        public int NrDecessi { get; set; }
        public int NrFeriti { get; set; }
        [MaxLength(250, ErrorMessage = "Last Name cannot be more than 250 characters")]
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
