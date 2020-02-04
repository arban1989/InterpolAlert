using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class EsitoFeRepository : IEsitoFeRepository
    {
        public IEnumerable<EsitoDto> GetEsiti()
        {
            IEnumerable<EsitoDto> esiti = new List<EsitoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("esiti");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EsitoDto>>();
                    readTask.Wait();

                    esiti = readTask.Result;
                }
            }

            return esiti;
        }

        public EsitoDto GetEsito(int esitoId)
        {
            EsitoDto esito = new EsitoDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"esiti/{esitoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EsitoDto>();
                    readTask.Wait();

                    esito = readTask.Result;
                }
            }

            return esito;
        }

        public EsitoDto GetEsitoOfAnEvent(int esitoId)
        {
            EsitoDto esito = new EsitoDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"esiti/eventi/{esitoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EsitoDto>();
                    readTask.Wait();

                    esito = readTask.Result;
                }
            }

            return esito;
        }

        public IEnumerable<EventoDto> GetEventiFromAnEsito(int esitoId)
        {
            IEnumerable<EventoDto> eventi = new List<EventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"esiti/{esitoId}/eventi");
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
    }
}
