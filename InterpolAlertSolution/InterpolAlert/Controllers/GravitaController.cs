using System;
using System.Collections.Generic;
using System.Linq;
using InterpolAlert.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InterpolAlertApi.Models;
using System.Net.Http;
using InterpolAlertApi.Dtos;

namespace InterpolAlert.Controllers
{
    public class GravitaController : Controller
    {
        IGravitaFeRepository _gravitaFeRepository;

        public GravitaController(IGravitaFeRepository gravitaFeRepository)
        {
            _gravitaFeRepository = gravitaFeRepository;
        }

        // GET: Gravita
        public ActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            var gravita = _gravitaFeRepository.GetGravita();
            if (gravita.Count() <= 0)
            {
                ViewBag.Message = "Si è verificato un problema durante il recupero della gravità" + "il database o la gravità non esiste";
            }
            return View(gravita);
        }

        // GET: Gravita/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Gravita/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gravita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gravita gravita)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("gravita", gravita);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var newGravitaTask = result.Content.ReadAsAsync<Gravita>();
                    newGravitaTask.Wait();

                    var newGravita = newGravitaTask.Result;
                    TempData["SuccessMessage"] = $"La gravita {newGravita.NomeGravita} è stata creata con successo. ";

                    return RedirectToAction("Index", "Gravita");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", $"Gravita {gravita.NomeGravita} esiste già!");
                }

                else
                {
                    ModelState.AddModelError("", "Sono sorti alcuni problemi. Gravita non è stata creata!");
                }
            }
            return View();
        }

        // GET: Gravita/Edit/5
        public ActionResult Edit(int gravitaId)
        {
            var gravitaToUpdate = _gravitaFeRepository.GetGravita(gravitaId);

            if (gravitaToUpdate == null)
            {
                ModelState.AddModelError("", "Errore durante l'ottenimento  Gravita");
                gravitaToUpdate = new GravitaDto();
            }

            return View(gravitaToUpdate);
        }

        // POST: Gravita/Edit/5
        [HttpPost]
        public ActionResult Edit(GravitaDto gravita)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PutAsJsonAsync($"gravita/{gravita.GravitaId}", gravita);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Gravita è stato aggiornato con successo.";

                    return RedirectToAction("Index", "Gravita");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", "Gravita Esiste già!");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Localita non aggiornato!");
                }
            }

            return View(gravita);
        }

        // GET: Gravita/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Gravita/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int gravitaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"gravita/{gravitaId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Gravita è stato eliminato con successo.";

                    return RedirectToAction("Index", "Gravita");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"Gravita non può essere cancellato perché " +
                                                $"è utilizzato da almeno un eventoo");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Gravita non è stato cancellato!");
                }
            }

            var gravitaListDto = _gravitaFeRepository.GetGravita();

            return View("Index", gravitaListDto);
        }
    }
}