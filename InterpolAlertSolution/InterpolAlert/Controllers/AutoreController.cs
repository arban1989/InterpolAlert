using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InterpolAlert.ModelsForView;
using InterpolAlert.Services;
using InterpolAlertApi.Dtos;
using InterpolAlertApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlert.Controllers
{
    public class AutoreController : Controller
    {
        private IAutoreFeRepository _autoreFeRepository;
        private IFazioneFeRepository _fazioneFeRepository;

        public AutoreController(IAutoreFeRepository autoreFeRepository, IFazioneFeRepository fazioneFeRepository)
        {
            _autoreFeRepository = autoreFeRepository;
            _fazioneFeRepository = fazioneFeRepository;
        }

        // GET: Autore
        public ActionResult Index()
        { 
            var autori = _autoreFeRepository.GetAutori();

            if (autori.Count() <= 0)
            {
                ViewBag.Message = "There was a problem retrieving autori from the database or no autore exists";
            }

            var autorefazione = new List<AutoreViewModel>();


            foreach (var autore in autori)
            {
                var fazione = _autoreFeRepository.GetFazioneOfAnAutore(autore.AutoreId);
                if (fazione == null)
                {
                    ModelState.AddModelError("", "Some kind of error getting fazione of an Autore");
                    ViewBag.Message += $"There was a problem retrieving fazione from the " +
                                    $"database or no fazione for aurore with id {autore.AutoreId} exists";
                    //fazione = new FazioneDto();
                }

                autorefazione.Add(new AutoreViewModel
                {
                    AutoreId = autore.AutoreId,
                    NomeAutore = autore.NomeAutore,
                    NoteVarie = autore.NoteVarie,
                    Pericolosita = autore.Pericolosita,
                    Fazione = fazione
                });
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(autorefazione);
        }

        // GET: Autore/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Autore/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autore/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int FazioneId, AutoreViewModel autoremodel)
        {
            using (var client = new HttpClient())
            {
                var fazioneDto = _fazioneFeRepository.GetFazione(FazioneId);

                if (fazioneDto == null || autoremodel == null)
                {
                    ModelState.AddModelError("", "Invalid Fazione or Autore. Cannot create Autore!");
                    return View(autoremodel);
                }

                var autore = new Autore{
                    NomeAutore = autoremodel.NomeAutore,
                    NoteVarie = autoremodel.NoteVarie,
                    Pericolosita = autoremodel.Pericolosita
                };



                autore.Fazione = new Fazione
                {
                    FazioneId = fazioneDto.FazioneId,
                    NomeFazione = fazioneDto.NomeFazione
                };

                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("autori", autore);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var newAutoreTask = result.Content.ReadAsAsync<Autore>();
                    newAutoreTask.Wait();

                    var newAutore = newAutoreTask.Result;
                    TempData["SuccessMessage"] = $"Autore {newAutore.NomeAutore}" +
                                                $"was successfully created.";
                    return RedirectToAction("Index", "Autore");
                }

                ModelState.AddModelError("", "Autore not created");
            }

            return View(autoremodel);
        }

        // GET: Autore/Edit/5
        public ActionResult Edit(int FazioneId, int autoreId)
        {
            var autoreDto = _autoreFeRepository.GetAutore(autoreId);
            var fazioneDto = _autoreFeRepository.GetFazioneOfAnAutore(autoreId);

            Autore autore = null;
            if (fazioneDto == null || autoreDto == null)
            {
                ModelState.AddModelError("", "Invalid Fazione or Autore. Cannot update Autore!");
                autore = new Autore();
            }
            else
            {
                autore = new Autore
                {
                    AutoreId = autoreDto.AutoreId,
                    NomeAutore = autoreDto.NomeAutore,
                    Pericolosita = autoreDto.Pericolosita,
                    NoteVarie = autoreDto.NoteVarie,
                    Fazione = new Fazione
                    {
                        FazioneId = fazioneDto.FazioneId,
                        NomeFazione = fazioneDto.NomeFazione
                    }
                };
            }

            return View(autore);
        }

        // POST: Autore/Edit/5
        [HttpPost]
        public ActionResult Edit(int fazioneId, Autore autoreToEdit)
        {
            var fazioneDto = _fazioneFeRepository.GetFazione(fazioneId);

            if (fazioneDto == null || autoreToEdit == null)
            {
                ModelState.AddModelError("", "Invalid Fazione, or Autore. Cannot update Autore!");
            }
            else
            {
                autoreToEdit.Fazione = new Fazione
                {
                    FazioneId = fazioneDto.FazioneId,
                    NomeFazione = fazioneDto.NomeFazione
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44357/api/");
                    var responseTask = client.PutAsJsonAsync($"autori/{autoreToEdit.AutoreId}", autoreToEdit);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Autore aggiornato con successo";
                        return RedirectToAction("Index", "Autore");
                    }

                    ModelState.AddModelError("", "Unexpected Error. Autore Not Updated");
                }
            }

            return View(autoreToEdit);
        }

        // GET: Autore/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Autore/Delete/5
        [HttpPost]
        public ActionResult Delete(int autoreId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"autori/{autoreId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Autore was successfully deleted.";

                    return RedirectToAction("Index");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"Autore cannot be deleted because " +
                                                $"it is used by at least one event");
                }
                else
                {
                    ModelState.AddModelError("", "Some kind of error. Autore not deleted!");
                }
            }

            var autori = _autoreFeRepository.GetAutori();

            if (autori.Count() <= 0)
            {
                ViewBag.Message = "There was a problem retrieving autori from the database or no autore exists";
            }

            var autorefazione = new List<AutoreViewModel>();


            foreach (var autore in autori)
            {
                var fazione = _autoreFeRepository.GetFazioneOfAnAutore(autore.AutoreId);
                if (fazione == null)
                {
                    ModelState.AddModelError("", "Some kind of error getting fazione of an Autore");
                    ViewBag.Message += $"There was a problem retrieving fazione from the " +
                                    $"database or no fazione for aurore with id {autore.AutoreId} exists";
                    //fazione = new FazioneDto();
                }

                autorefazione.Add(new AutoreViewModel
                {
                    AutoreId = autore.AutoreId,
                    NomeAutore = autore.NomeAutore,
                    NoteVarie = autore.NoteVarie,
                    Pericolosita = autore.Pericolosita,
                    Fazione = fazione
                });
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View("Index", autorefazione);
        }
    }
}