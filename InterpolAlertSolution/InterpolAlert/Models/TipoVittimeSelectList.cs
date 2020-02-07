using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Models
{
    public class TipoVittimeSelectList
    {
        public int TipoVittimaId { get; set; }
        public SelectList TipoVittimeList { get; set; }

    }
}
