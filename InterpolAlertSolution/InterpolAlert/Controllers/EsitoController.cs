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
                ViewBag.Message = "There was a problem retrieving  the esiti from" + "the database or no esiti exists";
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
                    TempData["SuccessMessage"] = $"L'esito {esito.NomeEsito} was successfully created. ";

                    return RedirectToAction("Index", "Esito");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", $"Esito {esito.NomeEsito} Already Exists!");
                }

                else
                {
                    ModelState.AddModelError("", "Some kind of error. Gravita not created!");
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
                ModelState.AddModelError("", "Error getting Esito");
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
                    TempData["SuccessMessage"] = $"Esito was successfully updated.";

                    return RedirectToAction("Index", "Esito");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", "Esito Already Exists!");
                }
                else
                {
                    ModelState.AddModelError("", "Some kind of error. Esito not updated!");
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
                    TempData["SuccessMessage"] = $"Esito was successfully deleted.";

                    return RedirectToAction("Index", "Esito");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"Esito cannot be deleted because " +
                                                $"it is used by at least one Evento");
                }
                else
                {
                    ModelState.AddModelError("", "Some kind of error. Esito not deleted!");
                }
            }

            return View("Index", "Esito");
        }
    }
}