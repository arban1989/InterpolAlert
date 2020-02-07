using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class Autore
    {
        [Key]
        public int AutoreId { get; set; }
        [Required]
        public string NomeAutore { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Il cognome non può contenere più di 250 caratteri")]
        public string Pericolosita { get; set; }
        public string NoteVarie { get; set; }

        public virtual ICollection<AutoriEventi> AutoriEventi { get; set; }
        public virtual Fazione Fazione { get; set; }

    }
}
