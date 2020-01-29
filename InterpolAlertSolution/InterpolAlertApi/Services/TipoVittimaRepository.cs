using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class TipoVittimaRepository : ITipoVittimaRepository
    {
        private DbContextInterpol _tipoVittimaContext;

        public TipoVittimaRepository(DbContextInterpol tipoVittimaContext)
        {
            _tipoVittimaContext = tipoVittimaContext;
        }

        public bool CreateTipoVittima(TipoVittima tipoVittima)
        {
            _tipoVittimaContext.Add(tipoVittima);
            return Save();
        }

        public bool DeleteTipoVittima(TipoVittima tipoVittima)
        {
            _tipoVittimaContext.Remove(tipoVittima);
            return Save();
        }

        public ICollection<Evento> GetEventiFromATipoVittima(int tipoVittimaId)
        {
            return _tipoVittimaContext.Eventi.Where(ev => ev.TipoVittima.TipoVittimaId == tipoVittimaId).ToList();
        }

        public ICollection<TipoVittima> GetTipiVittima()
        {
            return _tipoVittimaContext.TipoVittima.ToList();
        }

        public TipoVittima GetTipoVittima(int tipoVittimaId)
        {
            return _tipoVittimaContext.TipoVittima.Where(tv => tv.TipoVittimaId == tipoVittimaId).FirstOrDefault();
        }

        public TipoVittima GetTipoVittimaOfAnEvent(int eventoId)
        {
            return _tipoVittimaContext.Eventi.Where(ev => ev.EventoId == eventoId).Select(tv => tv.TipoVittima).FirstOrDefault();
        }

        public bool IsDuplicateTipoVittima(int tipoVittimaId, string nomeTipoVittima)
        {
            var tipovittima = _tipoVittimaContext.TipoVittima.Where(tv => tv.TipoVittimaId != tipoVittimaId && tv.NomeTipoVittima.Trim().ToUpper() == nomeTipoVittima.Trim().ToUpper()).FirstOrDefault();
            return tipovittima == null ? false : true;
        }

        public bool Save()
        {
            var saved = _tipoVittimaContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool TipoVittimaExists(int tipoVittimaId)
        {
            return _tipoVittimaContext.TipoVittima.Any(tv => tv.TipoVittimaId == tipoVittimaId);
        }

        public bool UpdateTipoVittima(TipoVittima tipoVittima)
        {
            _tipoVittimaContext.Update(tipoVittima);
            return Save();
        }
    }
}
