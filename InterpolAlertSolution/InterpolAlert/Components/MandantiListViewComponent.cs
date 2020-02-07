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
    public class MandantiListViewComponent : ViewComponent
    {
        IMandanteFeRepository _mandanteFeRepository;

        public MandantiListViewComponent(IMandanteFeRepository mandanteFeRepository)
        {
            _mandanteFeRepository = mandanteFeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var mandanti = _mandanteFeRepository.GetMandanti().
                                                OrderBy(c => c.NomeMandante).
                                                Select(x => new { Id = x.MandanteId, Value = x.NomeMandante });

            var mandantiList = new MandanteSelectList
            {
                MandantiList = new SelectList(mandanti, "Id", "Value")
            };

            return View("_MandantiList", mandantiList);
        }
    }
}
