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
        [StringLength(200, MinimumLength = 20, ErrorMessage = "Note must be between 20 and 200 characters")]
        public string NoteVarie { get; set; }
    }
}
