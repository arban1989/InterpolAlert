using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class EsitoRepository : IEsitoRepository
    {
        private DbContextInterpol _esitoContext;

        public EsitoRepository(DbContextInterpol dbContext)
        {
            _esitoContext = dbContext;
        }

        public bool CreateEsito(Esito esito)
        {
            _esitoContext.Add(esito);
            return Save();
        }

        public bool DeleteEsito(Evento evento)
        {
            _esitoContext.Remove(evento);
            return Save();
        }

        public bool EsitoExists(int esitoId)
        {
            return _esitoContext.Esiti.Any(e => e.IdEsito == esitoId);
        }

        public ICollection<Esito> GetEsiti()
        {
            return _esitoContext.Esiti.ToList();
        }

        public Esito GetEsito(int esitoId)
        {
            return _esitoContext.Esiti.Where(es => es.IdEsito == esitoId).FirstOrDefault();
        }

        public Esito GetEsitoOfAnEvent(int eventoId)
        {
            return _esitoContext.Eventi.Where(ev => ev.IdEvento == eventoId).Select(es => es.Esito).FirstOrDefault();
        }

        public ICollection<Evento> GetEventiFromAnEsito(int esitoId)
        {
            return _esitoContext.Eventi.Where(ev => ev.Esito.IdEsito == esitoId).ToList();
        }

        public bool IsDuplicateEsito(int esitoId, string nomeEsito)
        {
            var esito = _esitoContext.Esiti.Where(es => es.IdEsito != esitoId && es.NomeEsito.Trim().ToUpper() == nomeEsito.Trim().ToUpper()).FirstOrDefault();
            return esito == null ? false : true;
        }

        public bool Save()
        {
            var saved = _esitoContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateEsito(Evento evento)
        {
            _esitoContext.Update(evento);
            return Save();
        }
    }
}
