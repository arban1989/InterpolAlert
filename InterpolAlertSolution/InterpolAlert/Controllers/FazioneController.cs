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
    public class FazioneController : Controller
    {
        private IFazioneFeRepository _fazioneFeRepository;

        public FazioneController(IFazioneFeRepository fazioneFeRepository)
        {
            _fazioneFeRepository = fazioneFeRepository;
        }

        // GET: Fazione
        public ActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            var fazioni = _fazioneFeRepository.GetFazioni();
            if (fazioni.Count() <= 0)
            {
                ViewBag.Message = "Si è verificato un problema durante il recupero delle Fazioni" + "il database o nessuna Fazione esiste";
            }
            return View(fazioni);
        }

        // GET: Fazione/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Fazione/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fazione/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fazione fazione)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("fazioni", fazione);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var newFazioneTask = result.Content.ReadAsAsync<Fazione>();
                    newFazioneTask.Wait();

                    var newFazione = newFazioneTask.Result;
                    TempData["SuccessMessage"] = $"Fazione {newFazione.NomeFazione} è stato creato con successo. ";

                    return RedirectToAction("Index", "Fazione");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", $"Fazione {fazione.NomeFazione} Esiste già!");
                }

                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Fazione non creato!");
                }
            }
            return View();
        }

        // GET: Fazione/Edit/5
        public ActionResult Edit(int fazioneId)
        {
            var fazioneToUpdate = _fazioneFeRepository.GetFazione(fazioneId);

            if (fazioneToUpdate == null)
            {
                ModelState.AddModelError("", "Errore durante l'ottenimento della Fazione");
                fazioneToUpdate = new FazioneDto();
            }

            return View(fazioneToUpdate);
        }

        // POST: Fazione/Edit/5
        [HttpPost]
        public ActionResult Edit(FazioneDto fazioneModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PutAsJsonAsync($"fazioni/{fazioneModel.FazioneId}", fazioneModel);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Fazione è stato aggiornato con successo.";

                    return RedirectToAction("Index", "Fazione");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", "Fazione Esiste già!");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Fazione non aggiornato!");
                }
            }

            return View(fazioneModel);
        }

        // GET: Fazione/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Fazione/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int fazioneId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"fazioni/{fazioneId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Fazione è stato eliminato con successo.";

                    return RedirectToAction("Index", "Fazione");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"Fazione non può essere cancellato perché " +
                                                $"è utilizzato da almeno un Autore oppure un Mandante");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Fazione non è stato cancellato!");
                }
            }

            var fazioniListDto = _fazioneFeRepository.GetFazioni();

            return View("Index", fazioniListDto);
        }
    }
}