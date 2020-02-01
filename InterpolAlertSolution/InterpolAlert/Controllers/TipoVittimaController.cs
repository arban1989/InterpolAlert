﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpolAlert.Services;
using InterpolAlertApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlert.Controllers
{
    public class TipoVittimaController : Controller
    {
        ITipoVittimaFeRepository _tipoVittimaFeRepository;

        public TipoVittimaController(ITipoVittimaFeRepository tipoVittimaFeRepository)
        {
            _tipoVittimaFeRepository = tipoVittimaFeRepository;
        }

        // GET: TipoVittima
        public ActionResult Index()
        {
            var tipovictims = _tipoVittimaFeRepository.GetTipiVittima();
            if (tipovictims.Count()<= 0)
            {
                ViewBag.Message = "There was a problem retrieving type of victims from" + "the database or no type victims exists";
            }
            return View(tipovictims);
        }

        // GET: TipoVittima/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoVittima/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoVittima/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVittimaDto tipoVittima)
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

        // GET: TipoVittima/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoVittima/Edit/5
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

        // GET: TipoVittima/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoVittima/Delete/5
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