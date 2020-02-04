using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class FazioneRepository : IFazioneRepository
    {
        private DbContextInterpol _fazioneContext;

        public FazioneRepository(DbContextInterpol fazioneContext)
        {
            _fazioneContext = fazioneContext;
        }

        public bool CreateFazione(Fazione fazione)
        {
            _fazioneContext.Add(fazione);
            return Save();
        }

        public bool DeleteFazione(Fazione fazione)
        {
            _fazioneContext.Remove(fazione);
            return Save();
        }

        public bool FazioneExists(int fazioneId)
        {
            return _fazioneContext.Fazioni.Any(fa => fa.FazioneId == fazioneId);
        }

        public ICollection<Autore> GetAutoriFromAFazione(int fazioneId)
        {
            return _fazioneContext.Autori.Where(au=>au.Fazione.FazioneId == fazioneId).ToList();
        }

        public Fazione GetFazione(int fazioneId)
        {
            return _fazioneContext.Fazioni.Where(fa=>fa.FazioneId == fazioneId).FirstOrDefault();
        }

        public Fazione GetFazioneByAuthor(int authorId)
        {
            return _fazioneContext.Autori.Where(au=>au.AutoreId == authorId).Select(fa=>fa.Fazione).FirstOrDefault();
        }

        public ICollection<Fazione> GetFazioni()
        {
           return _fazioneContext.Fazioni.ToList();
        }

        public ICollection<Mandante> GetMandantiFromAFazione(int fazioneId)
        {
            return _fazioneContext.Mandanti.Where(ma=>ma.Fazione.FazioneId == fazioneId).ToList();
        }

        public bool IsDuplicateFazione(int fazioneId, string nomeFazione)
        {
            var fazione = _fazioneContext.Fazioni.Where(fa=>fa.FazioneId != fazioneId && fa.NomeFazione.Trim().ToUpper() == nomeFazione.Trim().ToUpper()).FirstOrDefault();
            return fazione == null ? false : true;
        }

        public bool Save()
        {
            var save = _fazioneContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateFazione(Fazione fazione)
        {
            _fazioneContext.Update(fazione);
            return Save();
        }
    }
}
