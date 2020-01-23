using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Dtos
{
    public class EventoDto
    {
        public int IdEvento { get; set; }
        public string NomeEvento { get; set; }
        public DateTime DataOraInizio { get; set; }
        public DateTime DataOraFine { get; set; }
        public int NrVittime { get; set; }
        public int NrDecessi { get; set; }
        public int NrFeriti { get; set; }
        public string NoteVarie { get; set; }
        public bool Mediatore { get; set; }
        public bool FFSpeciali { get; set; }
        public bool Polizia { get; set; }
        public bool VigiliDelFuoco { get; set; }
    }
}
