using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InterpolAlert.Services;
using InterpolAlertApi.Dtos;
using InterpolAlertApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlert.Controllers
{
    public class EsitoController : Controller
    {
        private IEsitoFeRepository  _esitoFeRepository;

        public EsitoController(IEsitoFeRepository esitoFeRepository)
        {
            _esitoFeRepository = esitoFeRepository;
        }

        // GET: Esito
        public ActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            var esiti = _esitoFeRepository.GetEsiti();
            if (esiti.Count() <= 0)
            {
                ViewBag.Message = "Si è verificato un problema durante il recupero dell'esiti" + "il database o esiti non esiste";
            }
            return View(esiti);
        }

        // GET: Esito/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Esito/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Esito/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Esito esito)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("esiti", esito);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var newEsitoTask = result.Content.ReadAsAsync<Esito>();
                    newEsitoTask.Wait();

                    var newEsito = newEsitoTask.Result;
                    TempData["SuccessMessage"] = $"L'esito {esito.NomeEsito} è stato creato con successo. ";

                    return RedirectToAction("Index", "Esito");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", $"Esito {esito.NomeEsito} esiste già!");
                }

                else
                {
                    ModelState.AddModelError("", "Sono sorti dei problemi. Esito non è stato eliminato!");
                }
            }
            return View();
        }

        // GET: Esito/Edit/5
        public ActionResult Edit(int esitoId)
        {
            var esitoToUpdate = _esitoFeRepository.GetEsito(esitoId);

            if (esitoToUpdate == null)
            {
                ModelState.AddModelError("", "Errore durante il recupero di Esito");
                esitoToUpdate = new EsitoDto();
            }

            return View(esitoToUpdate);
        }

        // POST: Esito/Edit/5
        [HttpPost]
        public ActionResult Edit(EsitoDto esito)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PutAsJsonAsync($"esiti/{esito.EsitoId}", esito);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Esito è stato aggiornato con successo.";

                    return RedirectToAction("Index", "Esito");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", "Esito esiste già!");
                }
                else
                {
                    ModelState.AddModelError("", "Sono sorti dei problemi. Autore non è stato aggiornato!");
                }
            }

            return View(esito);
        }

        // GET: Esito/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Esito/Delete/5
        [HttpPost]
        public ActionResult Delete(int esitoId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"esiti/{esitoId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Esito è stato eliminato con successo.";

                    return RedirectToAction("Index", "Esito");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"Esito non è stato eliminato perchè " +
                                                $"è usato da almeno un Evento");
                }
                else
                {
                    ModelState.AddModelError("", " è storto qualche tipo di errore. Esito non cancellato!");
                }
            }

            return View("Index", "Esito");
        }
    }
}