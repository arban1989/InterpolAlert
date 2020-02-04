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
    public class FazioniListViewComponent : ViewComponent
    {
        IFazioneFeRepository _fazioneFeRepository;

        public FazioniListViewComponent(IFazioneFeRepository fazioneFeRepository)
        {
            _fazioneFeRepository = fazioneFeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var fazioni = _fazioneFeRepository.GetFazioni().
                                                OrderBy(c => c.NomeFazione).
                                                Select(x => new { Id = x.FazioneId, Value = x.NomeFazione });

            var fazioniList = new FazioneSelectList
            {
                FazioniList = new SelectList(fazioni, "Id", "Value")
            };

            return View("_FazioniList", fazioniList);
        }
    }
}
