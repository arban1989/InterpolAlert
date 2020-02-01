using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpolAlert.Services;
using InterpolAlertApi.Dtos;
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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