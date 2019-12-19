using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class Fazione
    {
        [Key]
        public int IdFazione { get; set; }
        [Required]
        public string NomeFazione { get; set; }
        public virtual ICollection<Mandante> Mandanti { get; set; }
    }
}
