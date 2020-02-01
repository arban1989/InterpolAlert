using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class TipoEventoFeRepository : ITipoEventoFeRepository
    {
        public IEnumerable<EventoDto> GetEventiFromATipoEvento(int tipoEventoId)
        {
            IEnumerable<EventoDto> eventi = new List<EventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"tipoevento/{tipoEventoId}/eventi");
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

        public IEnumerable<TipoEventoDto> GetTipiEventi()
        {
            IEnumerable<TipoEventoDto> tipoVittime = new List<TipoEventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("tipoeventi");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TipoEventoDto>>();
                    readTask.Wait();

                    tipoVittime = readTask.Result;
                }
            }

            return tipoVittime;
        }

        public TipoEventoDto GetTipoEvento(int tipoEventoId)
        {
            TipoEventoDto tipoEvento = new TipoEventoDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"tipoEvento/{tipoEventoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TipoEventoDto>();
                    readTask.Wait();

                    tipoEvento = readTask.Result;
                }
            }

            return tipoEvento;
        }

        public TipoEventoDto GetTipoTipoEventoOfAnEvent(int tipoEventoId)
        {
            TipoEventoDto tipoEvento = new TipoEventoDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"tipoEvento/eventi/{tipoEventoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TipoEventoDto>();
                    readTask.Wait();

                    tipoEvento = readTask.Result;
                }
            }

            return tipoEvento;
        }
    }
}
