using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class GravitaFeRepository : IGravitaFeRepository
    {
        public IEnumerable<EventoDto> GetEventiFromAGravita(int GravitaId)
        {
            IEnumerable<EventoDto> eventi = new List<EventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"Gravita/{GravitaId}/eventi");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EventoDto>>();
                    readTask.Wait();

                    eventi = readTask.Result;
                }
            }

            return eventi;
        }

        public IEnumerable<GravitaDto> GetGravita()
        {
            IEnumerable<GravitaDto> gravita = new List<GravitaDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("gravita");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<GravitaDto>>();
                    readTask.Wait();

                    gravita = readTask.Result;
                }
            }

            return gravita;
        }

        public GravitaDto GetGravita(int GravitaId)
        {
            GravitaDto gravita = new GravitaDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"tipoVittime/{GravitaId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GravitaDto>();
                    readTask.Wait();

                    gravita = readTask.Result;
                }
            }

            return gravita;
        }

        public GravitaDto GetGravitaOfAnEvent(int GravitaId)
        {
            GravitaDto gravita = new GravitaDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"gravita/eventi/{GravitaId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GravitaDto>();
                    readTask.Wait();

                    gravita = readTask.Result;
                }
            }

            return gravita;
        }
    }
}
