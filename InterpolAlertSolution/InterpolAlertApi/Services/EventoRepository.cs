using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class EventoRepository : IEventoRepository
    {
        private DbContextInterpol _dbContext;

        public EventoRepository(DbContextInterpol dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateEvento(List<int> autoreId, Evento evento)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEvento(Evento evento)
        {
            throw new NotImplementedException();
        }

        public bool EventoExists(int eventoId)
        {
            return _dbContext.Eventi.Any(e => e.IdEvento == eventoId);
        }

        public ICollection<Evento> GetEventi()
        {
            return _dbContext.Eventi.ToList();
        }

        public Evento GetEvento(int eventoId)
        {
            return _dbContext.Eventi.Where(ev => ev.IdEvento == eventoId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateEvento(List<int> autoreId, Evento evento)
        {
            throw new NotImplementedException();
        }
    }
}
