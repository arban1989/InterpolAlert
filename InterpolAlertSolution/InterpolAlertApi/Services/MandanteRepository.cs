using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class MandanteRepository : IMandanteRepository
    {
        private DbContextInterpol _mandateContext;

        public MandanteRepository(DbContextInterpol mandateContext)
        {
            _mandateContext = mandateContext;
        }

        public bool CreateMandante(Mandante mandante)
        {
            _mandateContext.Add(mandante);
            return Save();
        }

        public bool DeleteMandante(Mandante mandante)
        {
            _mandateContext.Remove(mandante);
            return Save();
        }

        public ICollection<Evento> GetEventiFromAMandante(int mandanteId)
        {
            return _mandateContext.Eventi.Where(ev => ev.Mandante.IdMandante == mandanteId).ToList();
        }

        public Fazione GetFazioneByMandante(int mandanteId)
        {
            return _mandateContext.Mandanti.Where(ma => ma.IdMandante == mandanteId).Select(fa => fa.Fazione).FirstOrDefault();
        }

        public Mandante GetMandante(int mandanteId)
        {
            return _mandateContext.Mandanti.Where(ma => ma.IdMandante == mandanteId).FirstOrDefault();
        }

        public Mandante GetMandanteOfAnEvent(int eventoId)
        {
            return _mandateContext.Eventi.Where(ev => ev.IdEvento == eventoId).Select(ma => ma.Mandante).FirstOrDefault();
        }

        public ICollection<Mandante> GetMandanti()
        {
            return _mandateContext.Mandanti.ToList();
        }

        public bool IsDuplicateMandante(int mandanteId, string nomeMandante)
        {
            var mandante = _mandateContext.Mandanti.Where(ma => ma.IdMandante == mandanteId && ma.NomeMandante.Trim().ToUpper() == nomeMandante.Trim().ToUpper());
            return mandante == null ? false : true;
        }

        public bool MandanteExists(int mandanteId)
        {
            return _mandateContext.Mandanti.Any(ma => ma.IdMandante == mandanteId);
        }

        public bool Save()
        {
            var save = _mandateContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateMandante(Mandante mandante)
        {
            _mandateContext.Update(mandante);
            return Save();
        }
    }
}
