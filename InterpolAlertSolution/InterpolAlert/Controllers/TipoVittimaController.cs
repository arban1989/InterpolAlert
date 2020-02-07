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
            var tipoVittime = _tipoVittimaFeRepository.GetTipiVittima();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            if (tipoVittime.Count()<= 0)
            {
                ViewBag.Message = "There was a problem retrieving type of victims from the database or no type victims exists";
            }
            return View(tipoVittime);
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
        public ActionResult Create(TipoVittima tipoVittima)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PostAsJsonAsync("tipovittime", tipoVittima);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var newTipoVittimaTask = result.Content.ReadAsAsync<TipoVittima>();
                    newTipoVittimaTask.Wait();

                    var newTipoVittime = newTipoVittimaTask.Result;
                    TempData["SuccessMessage"] = $"Il tipo Vittima {newTipoVittime.NomeTipoVittima} was successfully created. ";

                    return RedirectToAction("Index", "TipoVittima");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", $"TipoVittima {tipoVittima.NomeTipoVittima} Already Exists!");
                }

                else
                {
                    ModelState.AddModelError("", "Some kind of error. TipoVittima not created!");
                }
            }
            return View();
        }

        // GET: TipoVittima/Edit/5
        public ActionResult Edit(int tipoVittimaId)
        {
            var tipoVittimaToUpdate = _tipoVittimaFeRepository.GetTipoVittima(tipoVittimaId);

            if (tipoVittimaToUpdate == null)
            {
                ModelState.AddModelError("", "Error getting Tipo Vittima");
                tipoVittimaToUpdate = new TipoVittimaDto();
            }

            return View(tipoVittimaToUpdate);
        }

        // POST: TipoVittima/Edit/5
        [HttpPost]
        public ActionResult Edit(TipoVittimaDto tipoVittima)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.PutAsJsonAsync($"tipovittime/{tipoVittima.TipoVittimaId}", tipoVittima);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"TipoVittima was successfully updated.";

                    return RedirectToAction("Index", "TipoVittima");
                }

                if ((int)result.StatusCode == 422)
                {
                    ModelState.AddModelError("", "TipoVittima Already Exists!");
                }
                else
                {
                    ModelState.AddModelError("", "Some kind of error. Localita not updated!");
                }
            }

            return View(tipoVittima);
        }

        // GET: TipoVittima/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: TipoVittima/Delete/5
        [HttpPost]
        public ActionResult Delete(int tipoVittimaId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");
                var responseTask = client.DeleteAsync($"tipovittime/{tipoVittimaId}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"TipoVittima was successfully deleted.";

                    return RedirectToAction("Index", "TipoVittima");
                }

                if ((int)result.StatusCode == 409)
                {
                    ModelState.AddModelError("", $"TipoVittima cannot be deleted because " +
                                                $"it is used by at least one Evento");
                }
                else
                {
                    ModelState.AddModelError("", "Some kind of error. TipoEvento not deleted!");
                }
            }

            var tipoVittimaListDto = _tipoVittimaFeRepository.GetTipiVittima();

            return View("Index", tipoVittimaListDto);
        }
    }
}