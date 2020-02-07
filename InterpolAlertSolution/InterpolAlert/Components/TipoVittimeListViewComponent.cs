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
    public class TipoVittimeListViewComponent : ViewComponent
    {
        ITipoVittimaFeRepository _tipoVittimaFeRepository;

        public TipoVittimeListViewComponent(ITipoVittimaFeRepository tipoVittimaFeRepository)
        {
            _tipoVittimaFeRepository = tipoVittimaFeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var tipoVittime = _tipoVittimaFeRepository.GetTipiVittima().
                                                OrderBy(c => c.NomeTipoVittima).
                                                Select(x => new { Id = x.TipoVittimaId, Value = x.NomeTipoVittima });

            var tipoVittimeList = new TipoVittimeSelectList
            {
                TipoVittimeList = new SelectList(tipoVittime, "Id", "Value")
            };

            return View("_TipoVittimeList", tipoVittimeList);
        }
    }
}
