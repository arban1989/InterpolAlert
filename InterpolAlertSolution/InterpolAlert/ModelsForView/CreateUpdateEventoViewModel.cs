using InterpolAlertApi.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.ModelsForView
{
    public class CreateUpdateEventoViewModel
    {
        public EventoDto Evento { get; set; }

        public List<int> listaAutoriId { get; set; }
        public List<SelectListItem> AutoriSelectListItems { get; set; }

        public int tipoVittimaId { get; set; }
        public int localitaId { get; set; }
        public int gravitaId { get; set; }
        public int esitoId { get; set; }
        public int tipoEventoId { get; set; }
        public int mandanteId { get; set; }

    }
}
