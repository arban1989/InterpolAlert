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
                ViewBag.Message = "There was a problem retrieving  the gravity from" + "the database or no gravity exists";
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
                    TempData["SuccessMessage"] = $"La gravita {newGravita.NomeGravita} was successfully created. ";

                    return RedirectToAction("Index", "Gravita");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", $"Gravita {gravita.NomeGravita} Already Exists!");
                }

                else
                {
                    ModelState.AddModelError("", "Some kind of error. Gravita not created!");
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
                ModelState.AddModelError("", "Error getting Gravita");
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
                    TempData["SuccessMessage"] = $"Gravita was successfully updated.";

                    return RedirectToAction("Index", "Gravita");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", "Gravita Already Exists!");
                }
                else
                {
                    ModelState.AddModelError("", "Some kind of error. Localita not updated!");
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
                    TempData["SuccessMessage"] = $"Gravita was successfully deleted.";

                    return RedirectToAction("Index", "Gravita");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"Gravita cannot be deleted because " +
                                                $"it is used by at least one Evento");
                }
                else
                {
                    ModelState.AddModelError("", "Some kind of error. Gravita not deleted!");
                }
            }

            return View("Index", "Gravita");
        }
    }
}