using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface ITipoEventoRepository
    {
        ICollection<TipoEvento> GetTipoEventi();
        TipoEvento GetTipoEvento(int tipoEventoId);
        TipoEvento GetTipoEventoOfAnEvent(int eventoId);
        ICollection<Evento> GetEventiFromATipoEvento(int tipoEventoId);
        bool TipoEventoExists(int tipoEventoId);
        bool IsDuplicateTipoEvento(int tipoEventoId, string nomeTipoEvento);
        bool CreateTipoEvento(TipoEvento tipoEvento);
        bool UpdateTipoEvento(TipoEvento tipoEvento);
        bool DeleteTipoEvento(TipoEvento tipoEvento);
        bool Save();
    }
}
