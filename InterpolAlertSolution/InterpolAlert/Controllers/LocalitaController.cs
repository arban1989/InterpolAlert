using System;
using System.Collections.Generic;
using System.Globalization;
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

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
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
        public ActionResult Create(LocalitaForCreate localitaModel)
        {
            Localita localita = new Localita()
            {
                Nazione = localitaModel.Nazione,
                NomeLocalita = localitaModel.NomeLocalita,
                Latitudine = Convert.ToDecimal(localitaModel.Latitudine),
                Longitudine = Convert.ToDecimal(localitaModel.Longitudine),
                LivelloRischio = localitaModel.LivelloRischio
            };


            //var d = Convert.ToDecimal(localitaModel.Latitudine.ToString(), new CultureInfo("en-US"));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("localita", localita);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var newLocalitaTask = result.Content.ReadAsAsync<Localita>();
                    newLocalitaTask.Wait();

                    var newLocalita = newLocalitaTask.Result;
                    TempData["SuccessMessage"] = $"Localita {newLocalita.NomeLocalita} è stato creato con successo.";

                    return RedirectToAction("Index", "Localita");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", $"Localita {localitaModel.NomeLocalita} Already Exists!");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Country non creato!");
                }
            }

            return View(localitaModel);
        }

        // GET: Localita/Edit/5
        public ActionResult Edit(int localitaId)
        {
            var localitaToUpdate = _localitaFeRepository.GetLocalita(localitaId);
            LocalitaForCreate localita = new LocalitaForCreate()
            {
                LocalitaId = localitaToUpdate.LocalitaId,
                Nazione = localitaToUpdate.Nazione,
                NomeLocalita = localitaToUpdate.NomeLocalita,
                Latitudine = localitaToUpdate.Latitudine.ToString(),
                Longitudine = localitaToUpdate.Longitudine.ToString(),
                LivelloRischio = localitaToUpdate.LivelloRischio
            };

            if (localita == null)
            {
                ModelState.AddModelError("", "Error getting localita");
                localita = new LocalitaForCreate();
            }

            return View(localita);
        }

        // POST: Localita/Edit/5
        [HttpPost]
        public ActionResult Edit(LocalitaForCreate localitaModel)
        {
            Localita localita = new Localita()
            {
                LocalitaId = localitaModel.LocalitaId,
                Nazione = localitaModel.Nazione,
                NomeLocalita = localitaModel.NomeLocalita,
                Latitudine = Convert.ToDecimal(localitaModel.Latitudine),
                Longitudine = Convert.ToDecimal(localitaModel.Longitudine),
                LivelloRischio = localitaModel.LivelloRischio
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PutAsJsonAsync($"localita/{localita.LocalitaId}", localita);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Localita è stato aggiornato con successo.";

                    return RedirectToAction("Index", "Localita");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", "Localita Already Exists!");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Localita not updated!");
                }
            }

            return View(localitaModel);
        }

        //GET: Localita/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Localita/Delete/5
        [HttpPost]
        public ActionResult Delete(int localitaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"localita/{localitaId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Localita è stato eliminato con successo.";

                    return RedirectToAction("Index", "Localita");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"La Localita cannot be deleted because " +
                                                $"it is used by at least one Evento");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Localita non è stato cancellato!");
                }
            }

            var localitaListDto = _localitaFeRepository.GetLocalitas();

            return View("Index", localitaListDto);
        }
    }
}