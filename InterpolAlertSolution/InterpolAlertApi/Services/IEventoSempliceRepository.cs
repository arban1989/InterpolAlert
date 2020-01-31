using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface IEventoSempliceRepository
    {
        ICollection<EventoSemplice> GetEventiSemplici();
        EventoSemplice GetEventoSemplice(int eventoSempliceId);
        bool EventoSempliceExists(int eventoSempliceId);
        bool CreateEventoSemplicec(EventoSemplice eventoSemplice);
        bool UpdateEventoSemplice(EventoSemplice eventoSemplice);
        bool DeleteEventoSemplice(EventoSemplice eventoSemplice);
        bool Save();
    }
}
