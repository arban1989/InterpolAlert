using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class EventoSempliceRepository : IEventoSempliceRepository
    {
        private DbContextInterpol _eventoSempliceContext;

        public EventoSempliceRepository(DbContextInterpol eventoSempliceContext)
        {
            _eventoSempliceContext = eventoSempliceContext;
        }

        public bool CreateEventoSemplice(EventoSemplice eventoSemplice)
        {
            _eventoSempliceContext.Add(eventoSemplice);
            return Save();
        }

        public bool DeleteEventoSemplice(EventoSemplice eventoSemplice)
        {
            _eventoSempliceContext.Remove(eventoSemplice);
            return Save();
        }

        public bool EventoSempliceExists(int eventoSempliceId)
        {
            return _eventoSempliceContext.EventoSemplice.Any(es=>es.EventoSempliceId == eventoSempliceId);
        }

        public ICollection<EventoSemplice> GetEventiSemplici()
        {
            return _eventoSempliceContext.EventoSemplice.ToList();
        }

        public EventoSemplice GetEventoSemplice(int eventoSempliceId)
        {
          return  _eventoSempliceContext.EventoSemplice.Where(es=>es.EventoSempliceId == eventoSempliceId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _eventoSempliceContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateEventoSemplice(EventoSemplice eventoSemplice)
        {
            _eventoSempliceContext.Update(eventoSemplice);
            return Save();
        }
    }
}
