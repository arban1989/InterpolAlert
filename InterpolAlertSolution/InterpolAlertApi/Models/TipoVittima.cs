using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class TipoVittima
    {
        [Key]
        public int TipoVittimaId { get; set; }
        [Required]
        public string NomeTipoVittima { get; set; }
        public virtual ICollection<Evento> Eventi { get; set; }

    }
}
