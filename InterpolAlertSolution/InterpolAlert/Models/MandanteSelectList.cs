using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Models
{
    public class MandanteSelectList
    {
        public int MandanteId { get; set; }
        public SelectList MandantiList { get; set; }
    }
}
