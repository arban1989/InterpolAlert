using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.ModelsForView
{
    public class MandanteViewModel
    {
        public int MandanteId { get; set; }
        [Required]
        public string NomeMandante { get; set; }
        [Required]
        public FazioneDto Fazione { get; set; }
    }
}
