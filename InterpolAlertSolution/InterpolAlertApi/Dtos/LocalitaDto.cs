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
        public string NomeLocalita { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Latitudine { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Longitudine { get; set; }
        public string Nazione { get; set; }
        public int LivelloRischio { get; set; }
    }
}
