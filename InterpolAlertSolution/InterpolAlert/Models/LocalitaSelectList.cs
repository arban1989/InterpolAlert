using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Models
{
    public class LocalitaSelectList
    {
        public int LocalitaId { get; set; }
        public SelectList LocalitaList { get; set; }
    }
}
