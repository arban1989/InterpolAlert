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
                ViewBag.Message = "Si è verificato un problema durante il recupero degli autori dal database o non esiste alcun autore";
            }

            var autorefazione = new List<AutoreViewModel>();


            foreach (var autore in autori)
            {
                var fazione = _autoreFeRepository.GetFazioneOfAnAutore(autore.AutoreId);
                if (fazione == null)
                {
                    ModelState.AddModelError("", "Si è verificato qualche problema per recuperare la fazione o l'autore");
                    ViewBag.Message += $"Si è verificato un problema durante il recupero della fazione da " +
                                    $"database o nessuna fazione per autore con ID { autore.AutoreId} esiste";
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
                    ModelState.AddModelError("", "Fazione o Autore sono invalidi. Autore non è stato creato!");
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
                                                $"è stato creato con successo.";
                    return RedirectToAction("Index", "Autore");
                }

                ModelState.AddModelError("", "Autore non è stato creato");
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
                ModelState.AddModelError("", "Fazione o Autore sono invalidi. Autore non è stato aggiornato!");
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
                ModelState.AddModelError("", "Fazione o Autore sono invalidi. Autore non è stato aggiornato!");
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

                    ModelState.AddModelError("", "Errore inaspettato. Autore non aggiornato");
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
                    TempData["SuccessMessage"] = $"Autore è stato eliminato con successo.";

                    return RedirectToAction("Index");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"Autore non è stato eliminato perchè" +
                                                $"è utilizzato per un'evento");
                }
                else
                {
                    ModelState.AddModelError("", "Sono sorti dei problemi. Autore non è stato eliminato!");
                }
            }

            return View("Index", "Autore");
        }
    }
}