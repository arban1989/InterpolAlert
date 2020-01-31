using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Dtos
{
    public class EventoSempliceDto
    {
        public int EventoSempliceId { get; set; }
        public string EventoSempliceNome { get; set; }
        public string EventoSempliceNote { get; set; }
        public DateTime EventoSempliceData { get; set; }
    }
}
