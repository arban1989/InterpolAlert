using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class TipoVittimaFeRepository : ITipoVittimaFeRepository
    {
        public IEnumerable<EventoDto> GetEventiFromATipoVittima(int tipoVittimaId)
        {
            IEnumerable<EventoDto> eventi = new List<EventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"tipoVittime/{tipoVittimaId}/eventi");
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

        public IEnumerable<TipoVittimaDto> GetTipiVittima()
        {
            IEnumerable<TipoVittimaDto> tipoVittime = new List<TipoVittimaDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("tipoVittime");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TipoVittimaDto>>();
                    readTask.Wait();

                    tipoVittime = readTask.Result;
                }
            }

            return tipoVittime;
        }

        public TipoVittimaDto GetTipoVittima(int tipoVittimaId)
        {
            TipoVittimaDto tipoVittima = new TipoVittimaDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"tipoVittime/{tipoVittimaId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TipoVittimaDto>();
                    readTask.Wait();

                    tipoVittima = readTask.Result;
                }
            }

            return tipoVittima;
        }

        public TipoVittimaDto GetTipoVittimaOfAnEvent(int eventoId)
        {
            TipoVittimaDto tipoVittima = new TipoVittimaDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"tipoVittime/eventi/{eventoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TipoVittimaDto>();
                    readTask.Wait();

                    tipoVittima = readTask.Result;
                }
            }

            return tipoVittima;
        }
    }
}
