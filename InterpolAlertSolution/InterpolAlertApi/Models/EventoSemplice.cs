using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class EventoSemplice
    {
        [Key]
        public int EventoSempliceId { get; set; }
        [Required]
        public string EventoSempliceNome { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "Note must be between 20 and 200 characters")]
        public string EventoSempliceNote { get; set; }
        [Required]
        public DateTime EventoSempliceData { get; set; }
    }
}
