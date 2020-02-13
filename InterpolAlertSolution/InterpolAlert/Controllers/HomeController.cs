using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InterpolAlert.Models;
using InterpolAlert.Services;
using InterpolAlert.ModelsForView;
using Newtonsoft.Json;

namespace InterpolAlert.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IEventoFeRepository _eventoFeRepository;
        private IAutoreFeRepository _autoreFeRepository;
        private IEsitoFeRepository _esitoFeRepository;
        private ITipoEventoFeRepository _tipoEventoFeRepository;
        private ITipoVittimaFeRepository _tipoVittimaFeRepository;
        private ILocalitaFeRepository _localitaFeRepository;
        private IGravitaFeRepository _gravitaFeRepository;
        private IMandanteFeRepository _mandanteFeRepository;

        public HomeController(ILogger<HomeController> logger, IEventoFeRepository eventoFeRepository, IAutoreFeRepository autoreFeRepository, IEsitoFeRepository esitoFeRepository, ITipoEventoFeRepository tipoEventoFeRepository, ITipoVittimaFeRepository tipoVittimaFeRepository, ILocalitaFeRepository localitaFeRepository, IGravitaFeRepository gravitaFeRepository, IMandanteFeRepository mandanteFeRepository)
        {
            _logger = logger;
            _eventoFeRepository = eventoFeRepository;
            _autoreFeRepository = autoreFeRepository;
            _esitoFeRepository = esitoFeRepository;
            _tipoEventoFeRepository = tipoEventoFeRepository;
            _tipoVittimaFeRepository = tipoVittimaFeRepository;
            _localitaFeRepository = localitaFeRepository;
            _gravitaFeRepository = gravitaFeRepository;
            _mandanteFeRepository = mandanteFeRepository;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var eventi = _eventoFeRepository.GetEventi();

            if (eventi.Count() <= 0)
            {
                ViewBag.Message = "Si è verificato un problema durante il recupero degli eventi dal database o non esiste alcun evento";
            }

            var eventoViewModel = new List<EventiForListViewModel>();

            foreach (var evento in eventi)
            {
                var autori = _autoreFeRepository.GetAutoriFromAnEvent(evento.EventoId).ToList();
                if (autori.Count() <= 0)
                    ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere autori");

                var esito = _esitoFeRepository.GetEsitoOfAnEvent(evento.EventoId);
                if (esito == null)
                    ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Esito del evento");

                var localita = _localitaFeRepository.GetLocalitaOfAnEvent(evento.EventoId);
                if (localita == null)
                    ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Localita del evento");

                var gravita = _gravitaFeRepository.GetGravitaOfAnEvent(evento.EventoId);
                if (gravita == null)
                    ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Gravita del evento");

                var tipoEvento = _tipoEventoFeRepository.GetTipoEventoOfAnEvent(evento.EventoId);
                if (tipoEvento == null)
                    ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere TipoEvento del evento");

                var mandante = _mandanteFeRepository.GetMandanteOfAnEvent(evento.EventoId);
                if (mandante == null)
                    ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Mandante del evento");


                eventoViewModel.Add(new EventiForListViewModel
                {
                    EventoId = evento.EventoId,
                    NomeEvento = evento.NomeEvento,
                    NoteVarie = evento.NoteVarie,
                    DataOraInizio = evento.DataOraInizio,
                    DataOraFine = evento.DataOraFine,
                    Autori = autori,
                    Localita = localita,
                    Mandante = mandante,
                    Esito = esito,
                    TipoEvento = tipoEvento,
                    Gravita = gravita,
                    Marker = new Marker
                    {
                        Citta = localita.NomeLocalita,
                        Latitudine = localita.Latitudine,
                        Longitudine = localita.Longitudine
                    } 
                });

            }

            eventoViewModel =  eventoViewModel.OrderByDescending(x => x.DataOraInizio).ToList();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(eventoViewModel);
        }

        //public JsonResult GetLocationForMap()
        //{
        //    var eventi = _eventoFeRepository.GetEventi();

        //    if (eventi.Count() <= 0)
        //    {
        //        ViewBag.Message = "Si è verificato un problema durante il recupero degli eventi dal database o non esiste alcun evento";
        //    }

        //    var eventoViewModel = new List<EventiForListViewModel>();

        //    var markerList = new List<Marker>();

        //    foreach (var evento in eventi)
        //    {
        //        var autori = _autoreFeRepository.GetAutoriFromAnEvent(evento.EventoId).ToList();
        //        if (autori.Count() <= 0)
        //            ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere autori");

        //        var esito = _esitoFeRepository.GetEsitoOfAnEvent(evento.EventoId);
        //        if (esito == null)
        //            ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Esito del evento");

        //        var localita = _localitaFeRepository.GetLocalitaOfAnEvent(evento.EventoId);
        //        if (localita == null)
        //            ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Localita del evento");

        //        var gravita = _gravitaFeRepository.GetGravitaOfAnEvent(evento.EventoId);
        //        if (gravita == null)
        //            ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Gravita del evento");

        //        var tipoEvento = _tipoEventoFeRepository.GetTipoEventoOfAnEvent(evento.EventoId);
        //        if (tipoEvento == null)
        //            ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere TipoEvento del evento");

        //        var mandante = _mandanteFeRepository.GetMandanteOfAnEvent(evento.EventoId);
        //        if (mandante == null)
        //            ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Mandante del evento");


        //        eventoViewModel.Add(new EventiForListViewModel
        //        {
        //            EventoId = evento.EventoId,
        //            NomeEvento = evento.NomeEvento,
        //            DataOraInizio = evento.DataOraInizio,
        //            DataOraFine = evento.DataOraFine,
        //            Autori = autori,
        //            Localita = localita,
        //            Mandante = mandante,
        //            Esito = esito,
        //            TipoEvento = tipoEvento,
        //            Gravita = gravita,
        //            Marker = new Marker
        //            {
        //                Citta = localita.NomeLocalita,
        //                Latitudine = localita.Latitudine.ToString(),
        //                Longitudine = localita.Longitudine.ToString()
        //            }
        //        });

        //        markerList.Add( new Marker { 
        //            Citta = localita.NomeLocalita,
        //            Latitudine = localita.Latitudine.ToString(),
        //            Longitudine = localita.Longitudine.ToString()
        //        });
        //    }
        //    return Json(markerList);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
