using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public interface IEventoSempliceFeRepository
    {
        IEnumerable<EventoSempliceDto> GetEventiSemplici();
        EventoSempliceDto GetEventoSemplice(int eventoSempliceId);
    }
}
