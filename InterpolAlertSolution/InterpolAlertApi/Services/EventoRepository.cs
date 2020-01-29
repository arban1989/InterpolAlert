using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class EventoRepository : IEventoRepository
    {
        private DbContextInterpol _eventoContext;

        public EventoRepository(DbContextInterpol eventoContext)
        {
            _eventoContext = eventoContext;
        }

        public bool CreateEvento(List<Autore> listaAutori, Evento evento)
        {

            foreach (var autore in listaAutori)
            {
                var autorevento = new AutoriEventi()
                {
                    Autore = autore,
                    Evento = evento
                };
                _eventoContext.Add(autorevento);

            }

            _eventoContext.Add(evento);

            return Save();
        }

        public bool DeleteEvento(Evento evento)
        {
            _eventoContext.Remove(evento);
            return Save();
        }

        public bool EventoExists(int eventoId)
        {
            return _eventoContext.Eventi.Any(e => e.EventoId == eventoId);
        }

        public ICollection<Evento> GetEventi()
        {
            return _eventoContext.Eventi.ToList();
        }

        public Evento GetEvento(int eventoId)
        {
            return _eventoContext.Eventi.Where(ev => ev.EventoId == eventoId).FirstOrDefault();
        }

        public bool IsDuplicateEvent(int eventoId, string nomeEvento)
        {
            var evento = _eventoContext.Eventi.Where(ev => ev.EventoId == eventoId && ev.NomeEvento.Trim().ToUpper() == nomeEvento.Trim().ToUpper());
            return evento == null ? false : true;
        }

        public bool Save()
        {
            var saved = _eventoContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateEvento(List<Autore> listaAutori, Evento evento)
        {
            var autorieventiToDelete = _eventoContext.AutoriEventi.Where(ae => ae.Evento.EventoId == evento.EventoId);

            _eventoContext.RemoveRange(autorieventiToDelete);

            foreach (var autore in listaAutori)
            {
                var autoreEvento = new AutoriEventi()
                {
                    Autore = autore,
                    Evento = evento
                };
                _eventoContext.Add(autoreEvento);

            }

            _eventoContext.Update(evento);

            return Save();
        }
    }
}
