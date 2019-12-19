﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Models
{
    public class TipoEvento
    {
        [Key]
        public int IdTipoEvento { get; set; }
        [Required]
        public string Tipo { get; set; }
    }
}
