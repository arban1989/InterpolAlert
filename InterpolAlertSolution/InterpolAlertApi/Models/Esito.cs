using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class Esito
    {
        [Key]
        public int IdEsito { get; set; }
        [Required]
        public string NomeEsito { get; set; }
    }
}
