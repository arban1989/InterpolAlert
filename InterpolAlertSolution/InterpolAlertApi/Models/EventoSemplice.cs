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
        public string EventoSempliceNome { get; set; }
        public string EventoSempliceNote { get; set; }
        public DateTime EventoSempliceData { get; set; }
    }
}
