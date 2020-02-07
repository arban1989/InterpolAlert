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
    public class LocalitaListViewComponent : ViewComponent
    {
        ILocalitaFeRepository _localitaFeRepository;

        public LocalitaListViewComponent(ILocalitaFeRepository localitaFeRepository)
        {
            _localitaFeRepository = localitaFeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var localitas = _localitaFeRepository.GetLocalitas().
                                                OrderBy(c => c.NomeLocalita).
                                                Select(x => new { Id = x.LocalitaId, Value = x.NomeLocalita });

            var localitaList = new LocalitaSelectList
            {
                LocalitaList = new SelectList(localitas, "Id", "Value")
            };

            return View("_LocalitaList", localitaList);
        }
    }
}
