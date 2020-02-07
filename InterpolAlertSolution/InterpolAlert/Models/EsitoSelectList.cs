using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Models
{
    public class EsitoSelectList
    {
        public int EsitoId { get; set; }
        public SelectList EsitiList { get; set; }
    }
}
