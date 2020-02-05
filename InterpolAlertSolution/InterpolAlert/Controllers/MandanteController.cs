using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InterpolAlert.ModelsForView;
using InterpolAlert.Services;
using InterpolAlertApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterpolAlert.Controllers
{
    public class MandanteController : Controller
    {
        private IMandanteFeRepository _mandanteFeRepository;
        private IFazioneFeRepository _fazioneFeRepository;

        public MandanteController(IMandanteFeRepository mandanteFeRepository, IFazioneFeRepository fazioneFeRepository)
        {
            _mandanteFeRepository = mandanteFeRepository;
            _fazioneFeRepository = fazioneFeRepository;
        }

        // GET: Mandante
        public ActionResult Index()
        {
            var mandanti = _mandanteFeRepository.GetMandanti();

            if (mandanti.Count() <= 0)
            {
                ViewBag.Message = "There was a problem retrieving autori from the database or no autore exists";
            }

            var mandantefazione = new List<MandanteViewModel>();
            foreach (var mandante in mandanti)
            {
                var fazione = _mandanteFeRepository.GetFazioneOfAMandante(mandante.MandanteId);
                if (fazione == null)
                {
                    ModelState.AddModelError("", "Some kind of error getting fazione of an Mandante");
                    ViewBag.Message += $"There was a problem retrieving fazione from the " +
                                    $"database or no fazione for mandate with id {mandante.MandanteId} exists";
                    //fazione = new FazioneDto();
                }

                mandantefazione.Add(new MandanteViewModel
                {
                    MandanteId = mandante.MandanteId,
                    NomeMandante = mandante.NomeMandante,
                    Fazione = fazione
                });
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(mandantefazione);
        }

        // GET: Mandante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mandante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mandante/Create
        [HttpPost]
        public ActionResult Create(int FazioneId, MandanteViewModel mandanteModel)
        {
            using (var client = new HttpClient())
            {
                var fazioneDto = _fazioneFeRepository.GetFazione(FazioneId);

                if (fazioneDto == null || mandanteModel == null)
                {
                    ModelState.AddModelError("", "Invalid Fazione or Mandante. Cannot create Mandante!");
                    return View(mandanteModel);
                }

                var mandante = new Mandante
                {
                    NomeMandante = mandanteModel.NomeMandante
                };



                mandante.Fazione = new Fazione
                {
                    FazioneId = fazioneDto.FazioneId,
                    NomeFazione = fazioneDto.NomeFazione
                };

                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("mandanti", mandante);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var newMandanteTask = result.Content.ReadAsAsync<Mandante>();
                    newMandanteTask.Wait();

                    var newMandante = newMandanteTask.Result;
                    TempData["SuccessMessage"] = $"Mandante {newMandante.NomeMandante}" +
                                                $"was successfully created.";
                    return RedirectToAction("Index", "Mandante");
                }

                ModelState.AddModelError("", "Mandante not created");
            }

            return View(mandanteModel);
        }

        // GET: Mandante/Edit/5
        public ActionResult Edit(int mandanteId)
        {
            var mandanteDto = _mandanteFeRepository.GetMandante(mandanteId);
            var fazioneDto = _mandanteFeRepository.GetFazioneOfAMandante(mandanteId);

            Mandante mandante = null;
            if (fazioneDto == null || mandanteDto == null)
            {
                ModelState.AddModelError("", "Invalid Fazione or Mandante. Cannot update Mandante!");
                mandante = new Mandante();
            }
            else
            {
                mandante = new Mandante
                {
                    MandanteId = mandanteDto.MandanteId,
                    NomeMandante = mandanteDto.NomeMandante,
                    Fazione = new Fazione
                    {
                        FazioneId = fazioneDto.FazioneId,
                        NomeFazione = fazioneDto.NomeFazione
                    }
                };
            }

            return View(mandante);
        }

        // POST: Mandante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int fazioneId, Mandante mandanteToEdit)
        {
            var fazioneDto = _fazioneFeRepository.GetFazione(fazioneId);

            if (fazioneDto == null || mandanteToEdit == null)
            {
                ModelState.AddModelError("", "Invalid Fazione, or Mandante. Cannot update Mandante!");
            }
            else
            {
                mandanteToEdit.Fazione = new Fazione
                {
                    FazioneId = fazioneDto.FazioneId,
                    NomeFazione = fazioneDto.NomeFazione
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44357/api/");
                    var responseTask = client.PutAsJsonAsync($"mandanti/{mandanteToEdit.MandanteId}", mandanteToEdit);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Mandante aggiornato con successo";
                        return RedirectToAction("Index", "Mandante");
                    }

                    ModelState.AddModelError("", "Unexpected Error. Mandante Not Updated");
                }
            }

            return View(mandanteToEdit);
        }

        // GET: Mandante/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Mandante/Delete/5
        [HttpPost]
        public ActionResult Delete(int mandanteId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"mandanti/{mandanteId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Mandante was successfully deleted.";

                    return RedirectToAction("Index");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"Mandante cannot be deleted because " +
                                                $"it is used by at least one event");
                }
                else
                {
                    ModelState.AddModelError("", "Some kind of error. Mandante not deleted!");
                }
            }

            return View("Index", "Mandante");
        }
    }
}