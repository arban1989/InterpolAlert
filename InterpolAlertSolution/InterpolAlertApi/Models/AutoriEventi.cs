using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class AutoriEventi
    {
        public int IdAutore { get; set; }
        public Autore Autore { get; set; }
        public int IdEvento { get; set; }
        public Evento Evento { get; set; }
    }
}
