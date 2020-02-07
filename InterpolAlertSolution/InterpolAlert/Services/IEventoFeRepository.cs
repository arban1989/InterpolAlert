using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public interface IEventoFeRepository
    {
        IEnumerable<EventoDto> GetEventi();
        EventoDto GetEvento(int eventoId);
    }
}
