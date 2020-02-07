using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Dtos
{
    public class AutoreDto
    {
        
        public int AutoreId { get; set; }
        [Required]
        public string NomeAutore { get; set; }
        [Required]
        public string Pericolosita { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "La nota deve contenere tra 20 e 200 caratteri")]
        public string NoteVarie { get; set; }
    }
}
