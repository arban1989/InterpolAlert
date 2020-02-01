using System;
using System.Collections.Generic;
using System.Linq;
using InterpolAlert.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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