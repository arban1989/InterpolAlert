using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public class TipoEventoRepository : ITipoEventoRepository
    {
        private DbContextInterpol _tipoEventoContext;

        public TipoEventoRepository(DbContextInterpol tipoEventoContext)
        {
            _tipoEventoContext = tipoEventoContext;
        }

        public bool CreateTipoEvento(TipoEvento tipoEvento)
        {
            _tipoEventoContext.Add(tipoEvento);
            return Save();
        }

        public bool DeleteTipoEvento(TipoEvento tipoEvento)
        {
            _tipoEventoContext.Remove(tipoEvento);
            return Save();
        }

        public ICollection<Evento> GetEventiFromATipoEvento(int tipoEventoId)
        {
            return _tipoEventoContext.Eventi.Where(ev => ev.TipoEvento.TipoEventoId == tipoEventoId).ToList();
        }

        public ICollection<TipoEvento> GetTipoEventi()
        {
            return _tipoEventoContext.TipoEventi.ToList();
        }

        public TipoEvento GetTipoEvento(int tipoEventoId)
        {
            return _tipoEventoContext.TipoEventi.Where(te => te.TipoEventoId == tipoEventoId).FirstOrDefault();
        }

        public TipoEvento GetTipoEventoOfAnEvent(int eventoId)
        {
            return _tipoEventoContext.Eventi.Where(ev => ev.EventoId == eventoId).Select(te => te.TipoEvento).FirstOrDefault();
        }

        public bool IsDuplicateTipoEvento(int tipoEventoId, string nomeTipoEvento)
        {
            var tipoevento = _tipoEventoContext.TipoEventi.Where(te => te.TipoEventoId != tipoEventoId && te.NomeTipoEvento.Trim().ToUpper() == nomeTipoEvento.Trim().ToUpper()).FirstOrDefault();
            return tipoevento == null ? false : true;
        }

        public bool Save()
        {
            var saved = _tipoEventoContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool TipoEventoExists(int tipoEventoId)
        {
            return _tipoEventoContext.TipoEventi.Any(te => te.TipoEventoId == tipoEventoId);
        }

        public bool UpdateTipoEvento(TipoEvento tipoEvento)
        {
            _tipoEventoContext.Update(tipoEvento);
            return Save();
        }
    }
}
