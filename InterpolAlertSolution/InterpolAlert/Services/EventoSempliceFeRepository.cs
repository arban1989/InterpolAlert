using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public class EventoSempliceFeRepository : IEventoSempliceFeRepository
    {
        public IEnumerable<EventoSempliceDto> GetEventiSemplici()
        {
            IEnumerable<EventoSempliceDto> eventisemplici = new List<EventoSempliceDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync("eventosemplice");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EventoSempliceDto>>();
                    readTask.Wait();

                    eventisemplici = readTask.Result;
                }
            }

            return eventisemplici;
        }

        public EventoSempliceDto GetEventoSemplice(int eventoSempliceId)
        {
            EventoSempliceDto eventosemplice = new EventoSempliceDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44357/api/");

                var response = client.GetAsync($"eventosemplice/{eventoSempliceId}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EventoSempliceDto>();
                    readTask.Wait();

                    eventosemplice = readTask.Result;
                }
            }

            return eventosemplice;
        }
    }
}
