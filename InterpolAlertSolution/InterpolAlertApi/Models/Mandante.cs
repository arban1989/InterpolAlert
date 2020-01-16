using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class Mandante
    {
        [Key]
        public int IdMandante { get; set; }
        [Required]
        public string NomeMandante { get; set; }
        public virtual ICollection<Evento> Eventi { get; set; }
        public virtual Fazione Fazione { get; set; }
    }
}
