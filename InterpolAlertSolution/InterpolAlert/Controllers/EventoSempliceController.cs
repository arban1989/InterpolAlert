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
    public class EventoSempliceController : Controller
    {
        private IEventoSempliceFeRepository _eventoSempliceFeRepository;

        public EventoSempliceController(IEventoSempliceFeRepository eventoSempliceFeRepository)
        {
            _eventoSempliceFeRepository = eventoSempliceFeRepository;
        }

        // GET: EventoSemplice
        public ActionResult Index(string searchString, int searchGravita)
        {
            ViewData["StringFilter"] = searchString;
            ViewData["IntFilter"] = searchGravita;
            var evententisemplici = _eventoSempliceFeRepository.GetEventiSemplici();
            if (!String.IsNullOrEmpty(searchString))
            {
                evententisemplici = evententisemplici.Where(ev => ev.EventoSempliceNome.ToLower().Contains(searchString.ToLower())
                                       || ev.EventoSempliceNote.ToLower().Contains(searchString.ToLower())).ToList();
            }
            if (searchGravita != 0)
            {
                evententisemplici = evententisemplici.Where(ev => ev.EventoSempliceGravita == searchGravita).ToList();
            }
            //return View(listaUtenti);
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(evententisemplici);
        }

        // GET: EventoSemplice/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventoSemplice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventoSemplice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventoSemplice eventoSemplice)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("eventosemplice", eventoSemplice);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var newEventoSempliceTask = result.Content.ReadAsAsync<EventoSemplice>();
                    newEventoSempliceTask.Wait();

                    var newEventoSemplice = newEventoSempliceTask.Result;
                    TempData["SuccessMessage"] = $"L'evento {newEventoSemplice.EventoSempliceNome} was successfully created.";

                    return RedirectToAction("Index", "EventoSemplice");
                }

                else
                {
                    ModelState.AddModelError("", "Some kind of error. Evento not created!");
                }
            }

            return View();
        }

        // GET: EventoSemplice/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventoSemplice/Edit/5
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

        // GET: EventoSemplice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventoSemplice/Delete/5
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