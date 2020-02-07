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
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
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
        public ActionResult Create(TipoEvento tipoEvento)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("tipoeventi", tipoEvento);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var newTipoEventoTask = result.Content.ReadAsAsync<TipoEvento>();
                    newTipoEventoTask.Wait();

                    var newTipoEvento = newTipoEventoTask.Result;
                    TempData["SuccessMessage"] = $"Il tipo Evento {newTipoEvento.NomeTipoEvento} è stato creato con successo. ";

                    return RedirectToAction("Index", "TipoEvento");
                }
                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", $"TipoEvento {tipoEvento.NomeTipoEvento} Already Exists!");
                }

                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. TipoEvento non creato!");
                }
            }
            return View();
        }

        // GET: TipoEvento/Edit/5
        public ActionResult Edit(int tipoEventoId)
        {
            var tipoEventoToUpdate = _tipoEventoFeRepository.GetTipoEvento(tipoEventoId);

            if (tipoEventoToUpdate == null)
            {
                ModelState.AddModelError("", "Error getting tipoEvento");
                tipoEventoToUpdate = new TipoEventoDto();
            }

            return View(tipoEventoToUpdate);
        }

        // POST: TipoEvento/Edit/5
        [HttpPost]
        public ActionResult Edit(TipoEventoDto tipoEvento)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PutAsJsonAsync($"tipoeventi/{tipoEvento.TipoEventoId}", tipoEvento);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"TipoEvento è stato aggiornato con successo.";

                    return RedirectToAction("Index", "TipoEvento");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", "TipoEvento Already Exists!");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. Localita not updated!");
                }
            }

            return View(tipoEvento);
        }

        // GET: TipoEvento/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: TipoEvento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int tipoEventoId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"tipoeventi/{tipoEventoId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"TipoEvento è stato eliminato con successo.";

                    return RedirectToAction("Index", "TipoEvento");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"TipoEvento cannot be deleted because " +
                                                $"it is used by at least one Evento");
                }
                else
                {
                    ModelState.AddModelError("", "Qualche tipo di errore. TipoEvento non è stato cancellato!");
                }
            }

            var tipoEventoListDto = _tipoEventoFeRepository.GetTipiEventi();

            return View("Index", tipoEventoListDto);
        }
    }
}