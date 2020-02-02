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
    public class TipoEventoController : Controller
    {
        ITipoEventoFeRepository _tipoEventoFeRepository;

        public TipoEventoController(ITipoEventoFeRepository tipoEventoFeRepository)
        {
            _tipoEventoFeRepository = tipoEventoFeRepository;
        }

        // GET: TipoEvento
        public ActionResult Index()
        {
            var tipo = _tipoEventoFeRepository.GetTipoEvento(2);

            var tipoeventi = _tipoEventoFeRepository.GetTipiEventi();
            if (tipoeventi.Count()<=0)
            {
                ViewBag.Message = "There was a problem retrieving type of event from" + "the database or no type event exists";
            }
            return View(tipoeventi);
        }

        // GET: TipoEvento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoEvento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoEvento tipoEvento)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("tipoevento", tipoEvento);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var newTipoEventoTask = result.Content.ReadAsAsync<TipoEvento>();
                    newTipoEventoTask.Wait();

                    var newTipoEvento = newTipoEventoTask.Result;
                    TempData["SuccessMessage"] = $"Il tipo Evento{newTipoEvento.NomeTipoEvento}was successfully created. ";

                    return RedirectToAction("Index", "Tipoevento");
                }

                else
                {
                    ModelState.AddModelError("", "Some kind of error. TipoEvento not created!");
                }
            }
            return View();
        }

        // GET: TipoEvento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoEvento/Edit/5
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

        // GET: TipoEvento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoEvento/Delete/5
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