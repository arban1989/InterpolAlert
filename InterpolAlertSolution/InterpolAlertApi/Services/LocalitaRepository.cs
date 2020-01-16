using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class LocalitaRepository : ILocalitaRepository
    {
        private DbContextInterpol _localitaContext;

        public LocalitaRepository(DbContextInterpol localitaContext)
        {
            _localitaContext = localitaContext;
        }

        public bool CreateLocalita(Localita localita)
        {
            _localitaContext.Add(localita);
            return Save();
        }

        public bool DeleteLocalita(Localita localita)
        {
            _localitaContext.Remove(localita);
            return Save();
        }

        public ICollection<Evento> GetEventiFromALocalita(int localitaId)
        {
            return _localitaContext.Eventi.Where(ev => ev.Localita.IdLocalita == localitaId).ToList();
        }

        public Localita GetLocalita(int localitaId)
        {
            return _localitaContext.Localita.Where(l => l.IdLocalita == localitaId).FirstOrDefault();
        }

        public ICollection<Localita> GetLocalitas()
        {
            return _localitaContext.Localita.ToList();
        }

        public Localita GetTipoLocalitaOfAnEvent(int eventoId)
        {
            return _localitaContext.Eventi.Where(ev => ev.IdEvento == eventoId).Select(l => l.Localita).FirstOrDefault();
        }

        public bool IsDuplicateLocalita(int localitaId, string nomeLocalita)
        {
            var localita = _localitaContext.Localita.Where(l => l.IdLocalita == localitaId && l.NomeLocalita.Trim().ToUpper() == nomeLocalita.Trim().ToUpper());
            return localita == null ? false : true;
        }

        public bool LocalitaExists(int localitaId)
        {
            return _localitaContext.Localita.Any(l => l.IdLocalita == localitaId);
        }

        public bool Save()
        {
            var saved = _localitaContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateLocalita(Localita localita)
        {
            _localitaContext.Update(localita);
            return Save();
        }
    }
}
