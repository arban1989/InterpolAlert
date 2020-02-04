using System;
using System.Collections.Generic;
using System.Linq;
using InterpolAlert.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InterpolAlertApi.Models;
using System.Net.Http;

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
            var gravita = _gravitaFeRepository.GetGravita();
            if (gravita.Count()<=0)
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
                    TempData["SuccessMessage"] = $"La gravita{newGravita.NomeGravita}was successfully created. ";

                    return RedirectToAction("Index", "Gravita");
                }

                else
                {
                    ModelState.AddModelError("", "Some kind of error. Gravita not created!");
                }
            }
                return View();   
        }

        // GET: Gravita/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Gravita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Gravita/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Gravita/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}