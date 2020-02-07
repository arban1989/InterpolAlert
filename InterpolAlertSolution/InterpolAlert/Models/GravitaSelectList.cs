using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Models
{
    public class GravitaSelectList
    {
        public int GravitaId { get; set; }
        public SelectList GravitaList { get; set; }
    }
}
