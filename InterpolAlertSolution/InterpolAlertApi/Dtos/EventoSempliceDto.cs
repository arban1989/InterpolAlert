using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Dtos
{
    public class EventoSempliceDto
    {
        public int EventoSempliceId { get; set; }
        [Required]
        public string EventoSempliceNome { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Il valore della Gravita deve essere tra 1 e 5")]
        public int EventoSempliceGravita { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "La nota deve contenere tra 20 e 200 caratteri")]
        public string EventoSempliceNote { get; set; }
        [Required]
        public DateTime EventoSempliceData { get; set; }
    }
}
