using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class EventoFeRepository : IEventoFeRepository
    {
        public IEnumerable<EventoDto> GetEventi()
        {
            IEnumerable<EventoDto> eventi = new List<EventoDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("eventi");
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

        public EventoDto GetEvento(int eventoId)
        {
            EventoDto evento = new EventoDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"eventi/{eventoId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EventoDto>();
                    readTask.Wait();

                    evento = readTask.Result;
                }
            }

            return evento;
        }
    }
}
