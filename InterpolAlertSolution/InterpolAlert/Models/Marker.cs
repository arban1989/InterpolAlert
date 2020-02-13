using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Models
{
    public class Marker
    {
        public string Citta { get; set; }
        public decimal Latitudine { get; set; }
        public decimal Longitudine { get; set; }
    }
}
