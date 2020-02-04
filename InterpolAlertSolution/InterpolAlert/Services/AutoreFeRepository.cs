using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class AutoreFeRepository : IAutoreFeRepository
    {
        public AutoreDto GetAutore(int autoreId)
        {
            AutoreDto autore = new AutoreDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"autori/{autoreId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AutoreDto>();
                    readTask.Wait();

                    autore = readTask.Result;
                }
            }

            return autore;
        }

        public IEnumerable<AutoreDto> GetAutori()
        {
            IEnumerable<AutoreDto> autori = new List<AutoreDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("autori");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AutoreDto>>();
                    readTask.Wait();

                    autori = readTask.Result;
                }
            }

            return autori;
        }

        public IEnumerable<AutoreDto> GetAutoriFromAnEvent(int eventoId)
        {
            IEnumerable<AutoreDto> autori = new List<AutoreDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"autori/eventi/{eventoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AutoreDto>>();
                    readTask.Wait();

                    autori = readTask.Result;
                }
            }

            return autori;
        }

        public IEnumerable<EventoDto> GetEventiOfAnAutore(int autoreId)
        {
            IEnumerable<EventoDto> eventi = new List<EventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"autori/{autoreId}/eventi");
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

        public FazioneDto GetFazioneOfAnAutore(int autoreId)
        {
            FazioneDto fazione = new FazioneDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"autori/{autoreId}/fazione");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<FazioneDto>();
                    readTask.Wait();

                    fazione = readTask.Result;
                }
            }

            return fazione;
        }
    }
}
