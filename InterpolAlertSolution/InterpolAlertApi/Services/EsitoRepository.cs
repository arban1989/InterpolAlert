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

        public bool DeleteEsito(Esito esito)
        {
            _esitoContext.Remove(esito);
            return Save();
        }

        public bool EsitoExists(int esitoId)
        {
            return _esitoContext.Esiti.Any(e => e.EsitoId == esitoId);
        }

        public ICollection<Esito> GetEsiti()
        {
            return _esitoContext.Esiti.ToList();
        }

        public Esito GetEsito(int esitoId)
        {
            return _esitoContext.Esiti.Where(es => es.EsitoId == esitoId).FirstOrDefault();
        }

        public Esito GetEsitoOfAnEvent(int eventoId)
        {
            return _esitoContext.Eventi.Where(ev => ev.EventoId == eventoId).Select(es => es.Esito).FirstOrDefault();
        }

        public ICollection<Evento> GetEventiFromAnEsito(int esitoId)
        {
            return _esitoContext.Eventi.Where(ev => ev.Esito.EsitoId == esitoId).ToList();
        }

        public bool IsDuplicateEsito(int esitoId, string nomeEsito)
        {
            var esito = _esitoContext.Esiti.Where(es => es.EsitoId != esitoId && es.NomeEsito.Trim().ToUpper() == nomeEsito.Trim().ToUpper()).FirstOrDefault();
            return esito == null ? false : true;
        }

        public bool Save()
        {
            var saved = _esitoContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateEsito(Esito esito)
        {
            _esitoContext.Update(esito);
            return Save();
        }
    }
}
