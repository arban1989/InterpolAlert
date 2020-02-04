using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlert.Controllers
{
    public class InterpolController : Controller
    {
        // GET: Interpol
        public ActionResult Index()
        {
            return View();
        }

        // GET: Interpol/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Interpol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Interpol/Create
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

        // GET: Interpol/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Interpol/Edit/5
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

        // GET: Interpol/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Interpol/Delete/5
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