using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.ModelsForView
{
    public class LocalitaForCreate
    {
        public int LocalitaId { get; set; }
        [Required]
        public string NomeLocalita { get; set; }
        [Required]
        public string Latitudine { get; set; }
        [Required]
        public string Longitudine { get; set; }
        [Required]
        public string Nazione { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Livello Rischio deve essere compreso tra 1 e 5")]
        public int LivelloRischio { get; set; }
    }
}
