using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class Localita
    {
        [Key]
        public int LocalitaId { get; set; }
        [Required]
        public string NomeLocalita { get; set; }
        [Required]
        public decimal Latitudine { get; set; }
        [Required]
        public decimal Longitudine { get; set; }
        [Required]
        public string Nazione { get; set; }
        [Required]
        public int LivelloRischio { get; set; }
        public virtual ICollection<Evento> Eventi { get; set; }


    }
}
