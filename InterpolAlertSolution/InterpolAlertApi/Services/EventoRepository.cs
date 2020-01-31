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

        public bool CreateEvento(List<int> listaAutoriId, int tipoVittimaId, int localitaId, int gravitaId, int esitoId, int tipoEventoId, int mandanteId, Evento evento)
        {
            var autori = _eventoContext.Autori.Where(a => listaAutoriId.Contains(a.AutoreId)).ToList();

            foreach (var autore in autori)
            {
                var autoriEventi = new AutoriEventi()
                {
                    Autore = autore,
                    Evento = evento
                };
                _eventoContext.Add(autoriEventi);
            }

            evento.Localita = _eventoContext.Localita.Where(l => l.LocalitaId == localitaId).FirstOrDefault();
            evento.TipoVittima = _eventoContext.TipoVittima.Where(tv => tv.TipoVittimaId == tipoVittimaId).FirstOrDefault();
            evento.Gravita = _eventoContext.Gravita.Where(g => g.GravitaId == gravitaId).FirstOrDefault();
            evento.Esito = _eventoContext.Esiti.Where(e => e.EsitoId == esitoId).FirstOrDefault();
            evento.TipoEvento = _eventoContext.TipoEventi.Where(te => te.TipoEventoId == tipoEventoId).FirstOrDefault();
            evento.Mandante = _eventoContext.Mandanti.Where(m => m.MandanteId == mandanteId).FirstOrDefault();


            _eventoContext.Add(evento);

            return Save();
        }

        public bool DeleteEvento(Evento evento)
        {
            var autoriEventiToDelete = _eventoContext.AutoriEventi.Where(au => au.EventoId == evento.EventoId);
            _eventoContext.RemoveRange(autoriEventiToDelete);
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

        public bool UpdateEvento(List<int> listaAutoriId, int tipoVittimaId, int localitaId, int gravitaId, int esitoId, int tipoEventoId, int mandanteId, Evento evento)
        {
            var autori = _eventoContext.Autori.Where(a => listaAutoriId.Contains(a.AutoreId)).ToList();

            var autoriEventiToDelete = _eventoContext.AutoriEventi.Where(au => au.EventoId == evento.EventoId);

            _eventoContext.RemoveRange(autoriEventiToDelete);

            foreach (var autore in autori)
            {
                var autoriEventi = new AutoriEventi()
                {
                    Autore = autore,
                    Evento = evento
                };
                _eventoContext.Add(autoriEventi);
            }

            evento.Localita = _eventoContext.Localita.Where(l => l.LocalitaId == localitaId).FirstOrDefault();
            evento.TipoVittima = _eventoContext.TipoVittima.Where(tv => tv.TipoVittimaId == tipoVittimaId).FirstOrDefault();
            evento.Gravita = _eventoContext.Gravita.Where(g => g.GravitaId == gravitaId).FirstOrDefault();
            evento.Esito = _eventoContext.Esiti.Where(e => e.EsitoId == esitoId).FirstOrDefault();
            evento.TipoEvento = _eventoContext.TipoEventi.Where(te => te.TipoEventoId == tipoEventoId).FirstOrDefault();
            evento.Mandante = _eventoContext.Mandanti.Where(m => m.MandanteId == mandanteId).FirstOrDefault();

            _eventoContext.Update(evento);

            return Save();
        }
    }
}
