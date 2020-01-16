using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Dtos
{
    public class LocalitaDto
    {
        public int IdLocalita { get; set; }
        public string NomeLocalita { get; set; }
        public decimal Latitudine { get; set; }
        public decimal Longitudine { get; set; }
        public string Nazione { get; set; }
        public int LivelloRischio { get; set; }
    }
}
