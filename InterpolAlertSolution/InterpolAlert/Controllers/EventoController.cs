using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InterpolAlert.Components;
using InterpolAlert.ModelsForView;
using InterpolAlert.Services;
using InterpolAlertApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlert.Controllers
{
    public class EventoController : Controller
    {
        private IEventoFeRepository _eventoFeRepository;
        private IAutoreFeRepository _autoreFeRepository;
        private IEsitoFeRepository _esitoFeRepository;
        private ITipoEventoFeRepository _tipoEventoFeRepository;
        private ITipoVittimaFeRepository _tipoVittimaFeRepository;
        private ILocalitaFeRepository _localitaFeRepository;
        private IGravitaFeRepository _gravitaFeRepository;
        private IMandanteFeRepository _mandanteFeRepository;

        public EventoController(IEventoFeRepository eventoFeRepository, IAutoreFeRepository autoreFeRepository, IEsitoFeRepository esitoFeRepository, ITipoEventoFeRepository tipoEventoFeRepository, ITipoVittimaFeRepository tipoVittimaFeRepository, ILocalitaFeRepository localitaFeRepository, IGravitaFeRepository gravitaFeRepository, IMandanteFeRepository mandanteFeRepository)
        {
            _eventoFeRepository = eventoFeRepository;
            _autoreFeRepository = autoreFeRepository;
            _esitoFeRepository = esitoFeRepository;
            _tipoEventoFeRepository = tipoEventoFeRepository;
            _tipoVittimaFeRepository = tipoVittimaFeRepository;
            _localitaFeRepository = localitaFeRepository;
            _gravitaFeRepository = gravitaFeRepository;
            _mandanteFeRepository = mandanteFeRepository;
        }



        // GET: Evento
        public ActionResult Index()
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
                    DataOraInizio = evento.DataOraInizio,
                    DataOraFine = evento.DataOraFine,
                    Autori = autori,
                    Localita = localita,
                    Mandante = mandante,
                    Esito = esito,
                    TipoEvento = tipoEvento,
                    Gravita = gravita
                });
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(eventoViewModel);
        }

        // GET: Evento/Details/5
        public ActionResult Details(int eventoId)
        {
            var evento = _eventoFeRepository.GetEvento(eventoId);
            if (evento == null)
            {
                ViewBag.Message = "Si è verificato un problema durante il recupero di questo Evento dal database o non esiste alcun evento";
            }

            var autori = _autoreFeRepository.GetAutoriFromAnEvent(evento.EventoId).ToList();
            if (autori.Count() <= 0)
                ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere autori");

            var esito = _esitoFeRepository.GetEsitoOfAnEvent(evento.EventoId);
            if (esito == null)
                ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Esito del evento");

            var tipoVittima = _tipoVittimaFeRepository.GetTipoVittimaOfAnEvent(evento.EventoId);
            if (tipoVittima == null)
                ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere Tipo vittima del evento");

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

            var eventoFullViewModel = new EventoViewModel
            {
                EventoId = evento.EventoId,
                NomeEvento = evento.NomeEvento,
                DataOraInizio = evento.DataOraInizio,
                DataOraFine = evento.DataOraFine,
                NrDecessi = evento.NrDecessi,
                NrVittime = evento.NrVittime,
                NrFeriti = evento.NrFeriti,
                NoteVarie = evento.NoteVarie,
                Mediatore = evento.Mediatore,
                FFSpeciali = evento.FFSpeciali,
                Polizia = evento.Polizia,
                VigiliDelFuoco = evento.VigiliDelFuoco,
                Autori = autori,
                Localita = localita,
                Mandante = mandante,
                Esito = esito,
                TipoEvento = tipoEvento,
                TipoVittima = tipoVittima,
                Gravita = gravita
            };

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(eventoFullViewModel);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            var autori = _autoreFeRepository.GetAutori();

            if (autori.Count() <= 0)
            {
                ModelState.AddModelError("", "Qualche tipo di errore nell'ottenere autori");
            }

            var listaAutori = new ListaAutori(autori.ToList());

            var createUpdateEvento = new CreateUpdateEventoViewModel
            {
                AutoriSelectListItems = listaAutori.GetListaAutori(),
            };

            return View(createUpdateEvento);
        }

        // POST: Evento/Create
        [HttpPost]
        public ActionResult Create(IEnumerable<int> listaAutoriId, int tipoVittimaId, int localitaId, int gravitaId, int esitoId, int tipoEventoId, int mandanteId, CreateUpdateEventoViewModel eventoToCreate)
        {
            using (var client = new HttpClient())
            {
                var evento = new Evento()
                {
                    NomeEvento = eventoToCreate.Evento.NomeEvento,
                    DataOraInizio = eventoToCreate.Evento.DataOraInizio,
                    DataOraFine = eventoToCreate.Evento.DataOraFine,
                    NrDecessi = eventoToCreate.Evento.NrDecessi,
                    NrVittime = eventoToCreate.Evento.NrVittime,
                    NrFeriti = eventoToCreate.Evento.NrFeriti,
                    NoteVarie = eventoToCreate.Evento.NoteVarie,
                    Mediatore = eventoToCreate.Evento.Mediatore,
                    FFSpeciali = eventoToCreate.Evento.FFSpeciali,
                    Polizia = eventoToCreate.Evento.Polizia,
                    VigiliDelFuoco = eventoToCreate.Evento.VigiliDelFuoco
                };

                var uriParameters = GetVirtualPropertyUri(listaAutoriId.ToList(), tipoVittimaId, localitaId, gravitaId, esitoId, tipoEventoId, mandanteId);

                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync($"eventi?{uriParameters}", evento);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTaskNewEvento = result.Content.ReadAsAsync<Evento>();
                    readTaskNewEvento.Wait();

                    var newEvento = readTaskNewEvento.Result;

                    TempData["SuccessMessage"] = $"Evento {evento.NomeEvento} è stato creato con successo.";
                    return RedirectToAction("Index", "Evento");
                }

                else
                {
                    ModelState.AddModelError("", "Errore! Evento non creato!");
                }
            }

            var listaAutori = new ListaAutori(_autoreFeRepository.GetAutori().ToList());
            eventoToCreate.AutoriSelectListItems = listaAutori.GetListaAutori(listaAutoriId.ToList());
            eventoToCreate.listaAutoriId = listaAutoriId.ToList();

            return View(eventoToCreate);
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int LocalitaId, int EsitoId, int GravitaId, int TipoEventoId, int TipoVittimaId, int MandanteId, int eventoId)
        {
            var eventoDto = _eventoFeRepository.GetEvento(eventoId);
            var listaAutori = new ListaAutori(_autoreFeRepository.GetAutori().ToList());
            var eventoViewModel = new CreateUpdateEventoViewModel
            {
                Evento = eventoDto,
                AutoriSelectListItems = listaAutori.GetListaAutori(_autoreFeRepository.GetAutoriFromAnEvent(eventoId)
                                        .Select(a => a.AutoreId).ToList()),
            };

            return View(eventoViewModel);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        public ActionResult Edit(IEnumerable<int> listaAutoriId, int tipoVittimaId, int localitaId, int gravitaId, 
            int esitoId, int tipoEventoId, int mandanteId, CreateUpdateEventoViewModel eventoToUpdate)
        {
            using (var client = new HttpClient())
            {
                var evento = new Evento()
                {
                    EventoId = eventoToUpdate.Evento.EventoId,
                    NomeEvento = eventoToUpdate.Evento.NomeEvento,
                    DataOraInizio = eventoToUpdate.Evento.DataOraInizio,
                    DataOraFine = eventoToUpdate.Evento.DataOraFine,
                    NrDecessi = eventoToUpdate.Evento.NrDecessi,
                    NrVittime = eventoToUpdate.Evento.NrVittime,
                    NrFeriti = eventoToUpdate.Evento.NrFeriti,
                    NoteVarie = eventoToUpdate.Evento.NoteVarie,
                    Mediatore = eventoToUpdate.Evento.Mediatore,
                    FFSpeciali = eventoToUpdate.Evento.FFSpeciali,
                    Polizia = eventoToUpdate.Evento.Polizia,
                    VigiliDelFuoco = eventoToUpdate.Evento.VigiliDelFuoco
                };

                var uriParameters = GetVirtualPropertyUri(listaAutoriId.ToList(), tipoVittimaId, localitaId, gravitaId, esitoId, tipoEventoId, mandanteId);

                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PutAsJsonAsync($"eventi/{evento.EventoId}?{uriParameters}", evento);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Evento {evento.NomeEvento} è stato aggiornato con successo.";
                    return RedirectToAction("Index", "Evento");
                }

                else
                {
                    ModelState.AddModelError("", "Errore! Evento non creato!");
                }
            }

            var listaAutori = new ListaAutori(_autoreFeRepository.GetAutori().ToList());
            eventoToUpdate.AutoriSelectListItems = listaAutori.GetListaAutori(listaAutoriId.ToList());
            eventoToUpdate.listaAutoriId = listaAutoriId.ToList();

            return View(eventoToUpdate);
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int eventoId)
        {
            var eventoDto = _eventoFeRepository.GetEvento(eventoId);

            return View(eventoDto);
        }

        // POST: Evento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string NomeEvento, int eventoId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"eventi/{eventoId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"L'evento {NomeEvento} è stato eliminato con successo.";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Qualche tipo di errore. Evento non è stato cancellato!");
            }

            var eventoDto = _eventoFeRepository.GetEvento(eventoId);
            return View(eventoDto);
        }

        private string GetVirtualPropertyUri(List<int> listaAutoriId, int tipoVittimaId, int localitaId, int gravitaId, int esitoId, int tipoEventoId, int mandanteId)
        {
            var uri = "";
            foreach (var autoreId in listaAutoriId)
            {
                uri += $"listaAutoriId={autoreId}&";
            }

            uri += $"tipoVittimaId={tipoVittimaId}&";

            uri += $"localitaId={localitaId}&";

            uri += $"gravitaId={gravitaId}&";

            uri += $"esitoId={esitoId}&";

            uri += $"tipoEventoId={tipoEventoId}&";

            uri += $"mandanteId={mandanteId}&";

            return uri;
        }
    }
}