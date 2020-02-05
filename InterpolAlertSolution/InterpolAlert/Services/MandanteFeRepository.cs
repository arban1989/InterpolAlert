using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class MandanteFeRepository : IMandanteFeRepository
    {
        public IEnumerable<EventoDto> GetEventiOfAMandante(int mandanteId)
        {
            IEnumerable<EventoDto> eventi = new List<EventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"mandanti/{mandanteId}/eventi");
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

        public FazioneDto GetFazioneOfAMandante(int mandanteId)
        {
            FazioneDto fazione = new FazioneDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"mandanti/{mandanteId}/fazione");
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

        public MandanteDto GetMandante(int mandanteId)
        {
            MandanteDto mandante = new MandanteDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"mandanti/{mandanteId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MandanteDto>();
                    readTask.Wait();

                    mandante = readTask.Result;
                }
            }

            return mandante;
        }

        public MandanteDto GetMandanteOfAnEvent(int eventoId)
        {
            MandanteDto mandante = new MandanteDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"mandanti/eventi/{eventoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MandanteDto>();
                    readTask.Wait();

                    mandante = readTask.Result;
                }
            }

            return mandante;
        }

        public IEnumerable<MandanteDto> GetMandanti()
        {
            IEnumerable<MandanteDto> mandanti = new List<MandanteDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("mandanti");
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
