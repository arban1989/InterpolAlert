using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Models
{
    public class FazioneSelectList
    {
        public int FazioneId { get; set; }
        public SelectList FazioniList { get; set; }
    }
}
