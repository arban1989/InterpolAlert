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
    public class EsitiListViewComponent : ViewComponent
    {
        IEsitoFeRepository _esitoFeRepository;

        public EsitiListViewComponent(IEsitoFeRepository esitoFeRepository)
        {
            _esitoFeRepository = esitoFeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var esiti = _esitoFeRepository.GetEsiti().
                                                OrderBy(c => c.NomeEsito).
                                                Select(x => new { Id = x.EsitoId, Value = x.NomeEsito });

            var esitiList = new EsitoSelectList
            {
                EsitiList = new SelectList(esiti, "Id", "Value")
            };

            return View("_EsitiList", esitiList);
        }
    }
}
