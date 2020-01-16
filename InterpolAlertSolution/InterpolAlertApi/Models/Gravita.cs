using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class Gravita
    {
        [Key]
        public int IdGravita { get; set; }
        [Required]
        public string NomeGravita { get; set; }
        public virtual ICollection<Evento> Eventi { get; set; }

    }
}
