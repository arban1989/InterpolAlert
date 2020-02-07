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
    public class GravitaListViewComponent : ViewComponent
    {
        IGravitaFeRepository _gravitaFeRepository;

        public GravitaListViewComponent(IGravitaFeRepository gravitaFeRepository)
        {
            _gravitaFeRepository = gravitaFeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var gravitas = _gravitaFeRepository.GetGravita().
                                                OrderBy(c => c.NomeGravita).
                                                Select(x => new { Id = x.GravitaId, Value = x.NomeGravita });

            var gravitaList = new GravitaSelectList
            {
                GravitaList = new SelectList(gravitas, "Id", "Value")
            };

            return View("_GravitaList", gravitaList);
        }
    }
}
