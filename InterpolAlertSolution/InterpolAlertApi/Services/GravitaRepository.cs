using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class GravitaRepository : IGravitaRepository
    {
        private DbContextInterpol _gravitaContext;

        public GravitaRepository(DbContextInterpol gravitaContext)
        {
            _gravitaContext = gravitaContext;
        }

        public bool CreateGravita(Gravita gravita)
        {
            _gravitaContext.Add(gravita);
            return Save();
        }

        public bool DeleteGravita(Gravita gravita)
        {
            _gravitaContext.Remove(gravita);
            return Save();
        }

        public ICollection<Evento> GetEventiFromAGravita(int gravitaId)
        {
            return _gravitaContext.Eventi.Where(ev => ev.Gravita.IdGravita == gravitaId).ToList();
        }

        public Gravita GetGravita(int gravitaId)
        {
            return _gravitaContext.Gravita.Where(gr => gr.IdGravita == gravitaId).FirstOrDefault();
        }

        public Gravita GetGravitaOfAnEvent(int gravitaId)
        {
            return _gravitaContext.Eventi.Where(ev => ev.IdEvento == gravitaId).Select(gr => gr.Gravita).FirstOrDefault();
        }

        public ICollection<Gravita> GetGravitas()
        {
            return _gravitaContext.Gravita.ToList();
        }

        public bool GravitaExists(int gravitaId)
        {
            return _gravitaContext.Gravita.Any(gr => gr.IdGravita == gravitaId);
        }

        public bool IsDuplicateGravita(int gravitaId, string nomeGravita)
        {
            var gravita = _gravitaContext.Gravita.Where(gr => gr.IdGravita == gravitaId && gr.NomeGravita.Trim().ToUpper() == nomeGravita.Trim().ToUpper());
            return gravita == null ? false : true;
        }

        public bool Save()
        {
            var saved = _gravitaContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateGravita(Gravita gravita)
        {
            _gravitaContext.Update(gravita);
            return Save();
        }
    }
}
