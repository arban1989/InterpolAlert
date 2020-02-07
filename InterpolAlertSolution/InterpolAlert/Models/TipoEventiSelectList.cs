using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Models
{
    public class TipoEventiSelectList
    {
        public int TipoEventoId { get; set; }
        public SelectList TipoEventiList { get; set; }
    }
}
