using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpolAlert.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlert.Controllers
{
    public class LocalitaController : Controller
    {
        ILocalitaFeRepository _localitaFeRepository;

        public LocalitaController(ILocalitaFeRepository localitaFeRepository)
        {
            _localitaFeRepository = localitaFeRepository;
        }


        // GET: Localita
        public ActionResult Index()
        {
            var localitas = _localitaFeRepository.GetLocalitas();
            if (localitas.Count() <= 0)
            {
                ViewBag.Message = "There was a problem retrieving Localitas from " +
                    "the database or no localita exists";
            }

            //ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(localitas);
        }

        // GET: Localita/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Localita/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Localita/Create
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

        // GET: Localita/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Localita/Edit/5
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

        // GET: Localita/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Localita/Delete/5
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