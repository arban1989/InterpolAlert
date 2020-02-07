using InterpolAlert.Models;
using InterpolAlert.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Components
{
    public class TipoEventiListViewComponent : ViewComponent
    {
        ITipoEventoFeRepository _tipoEventoFeRepository;

        public TipoEventiListViewComponent(ITipoEventoFeRepository tipoEventoFeRepository)
        {
            _tipoEventoFeRepository = tipoEventoFeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var tipoEventi = _tipoEventoFeRepository.GetTipiEventi().
                                                OrderBy(c => c.NomeTipoEvento).
                                                Select(x => new { Id = x.TipoEventoId, Value = x.NomeTipoEvento });

            var tipoEventiList = new TipoEventiSelectList
            {
                TipoEventiList = new SelectList(tipoEventi, "Id", "Value")
            };

            return View("_TipoEventiList", tipoEventiList);
        }
    }
}
