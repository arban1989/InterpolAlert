using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Dtos
{
    public class LocalitaDto
    {
        public int LocalitaId { get; set; }
        [Required]
        public string NomeLocalita { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Latitudine { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Longitudine { get; set; }
        [Required]
        public string Nazione { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Livello Rischio deve essere compreso tra 1 e 5")]
        public int LivelloRischio { get; set; }
    }
}
