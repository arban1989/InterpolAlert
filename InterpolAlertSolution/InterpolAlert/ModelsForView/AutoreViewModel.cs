using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.ModelsForView
{
    public class AutoreViewModel
    {
        public int AutoreId { get; set; }
        [Required]
        public string NomeAutore { get; set; }
        [Required]
        public string Pericolosita { get; set; }
        [Required]
        public string NoteVarie { get; set; }
        [Required]
        public FazioneDto Fazione { get; set; }
    }
}
