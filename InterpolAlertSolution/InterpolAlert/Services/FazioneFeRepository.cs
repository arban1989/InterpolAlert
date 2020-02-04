using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class FazioneFeRepository : IFazioneFeRepository
    {
        public IEnumerable<AutoreDto> GetAutoriFromAFazione(int fazioneId)
        {
            IEnumerable<AutoreDto> autori = new List<AutoreDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"fazioni/{fazioneId}/autori");
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

        public FazioneDto GetFazione(int fazioneId)
        {
            FazioneDto fazione = new FazioneDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"fazioni/{fazioneId}");
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

        public FazioneDto GetFazioneByAutore(int autoreId)
        {
            FazioneDto fazione = new FazioneDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"fazioni/autori/{autoreId}");
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

        public IEnumerable<FazioneDto> GetFazioni()
        {
            IEnumerable<FazioneDto> fazioni = new List<FazioneDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("fazioni");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<FazioneDto>>();
                    readTask.Wait();

                    fazioni = readTask.Result;
                }
            }

            return fazioni;
        }

        public IEnumerable<MandanteDto> GetMandantiFromAFazione(int fazioneId)
        {
            IEnumerable<MandanteDto> mandanti = new List<MandanteDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"fazioni/{fazioneId}/mandanti");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MandanteDto>>();
                    readTask.Wait();

                    mandanti = readTask.Result;
                }
            }

            return mandanti;
        }
    }
}
