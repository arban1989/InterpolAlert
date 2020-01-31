using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class LocalitaFeRepository : ILocalitaFeRepository
    {
        public IEnumerable<EventoDto> GetEventiFromALocalita(int localitaId)
        {
            IEnumerable<EventoDto> eventi = new List<EventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"localita/{localitaId}/eventi");
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

        public LocalitaDto GetLocalita(int localitaId)
        {
            LocalitaDto localita = new LocalitaDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"localita/{localitaId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<LocalitaDto>();
                    readTask.Wait();

                    localita = readTask.Result;
                }
            }

            return localita;
        }

        public LocalitaDto GetLocalitaOfAnEvent(int eventoId)
        {
            LocalitaDto localita = new LocalitaDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"localita/eventi/{eventoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<LocalitaDto>();
                    readTask.Wait();

                    localita = readTask.Result;
                }
            }

            return localita;
        }

        public IEnumerable<LocalitaDto> GetLocalitas()
        {
            IEnumerable<LocalitaDto> localitas = new List<LocalitaDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("localita");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<LocalitaDto>>();
                    readTask.Wait();

                    localitas = readTask.Result;
                }
            }

            return localitas;
        }
    }
}
