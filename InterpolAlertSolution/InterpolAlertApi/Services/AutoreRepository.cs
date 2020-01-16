using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class AutoreRepository : IAutoreRepository
    {
        private DbContextInterpol _autoreContext;

        public AutoreRepository(DbContextInterpol autoreContext)
        {
            _autoreContext = autoreContext;
        }

        public bool AutoreExists(int autoreId)
        {
            return _autoreContext.Autori.Any(au=>au.IdAutore == autoreId);
        }

        public bool CreateAutore(Autore autore)
        {
            _autoreContext.Add(autore);
            return Save();
        }

        public bool DeleteAutore(Autore autore)
        {
            _autoreContext.Remove(autore);
            return Save();
        }

        public ICollection<Autore> GetAllAutoriFromAnEvent(int eventoId)
        {
            return _autoreContext.AutoriEventi.Where(ae=>ae.IdEvento == eventoId).Select(au=>au.Autore).ToList();
        }

        public ICollection<Evento> GetAllEventiFromAnAutore(int autoreId)
        {
            return _autoreContext.AutoriEventi.Where(ae=>ae.IdAutore == autoreId).Select(ev=>ev.Evento).ToList();
        }

        public Autore GetAutore(int autoreId)
        {
            return _autoreContext.Autori.Where(au=>au.IdAutore == autoreId).FirstOrDefault();
        }

        public ICollection<Autore> GetAutori()
        {
            return _autoreContext.Autori.ToList();
        }

        public Fazione GetFazioneByAutore(int autoreId)
        {
            return _autoreContext.Autori.Where(au=>au.IdAutore == autoreId).Select(fa=>fa.Fazione).FirstOrDefault();
        }

        public bool IsDuplicateAutoreName(int autoreId, string nomeAutore)
        {
            var autore = _autoreContext.Autori.Where(au=>au.IdAutore == autoreId && au.NomeAutore.Trim().ToUpper() == nomeAutore.Trim().ToUpper());
            return autore == null ? false : true;
        }

        public bool Save()
        {
            var save = _autoreContext.SaveChanges();
            return save >= 0 ? true : false; 
        }

        public bool UpdateAutore(Autore autore)
        {
            _autoreContext.Update(autore);
            return Save();
        }
    }
}
