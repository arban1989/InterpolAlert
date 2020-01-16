using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface ITipoVittimaRepository
    {
        ICollection<TipoVittima> GetTipiVittima();
        TipoVittima GetTipoVittima(int tipoVittimaId);
        TipoVittima GetTipoVittimaOfAnEvent(int eventoId);
        ICollection<Evento> GetEventiFromATipoVittima(int tipoVittimaId);
        bool TipoVittimaExists(int tipoVittimaId);
        bool IsDuplicateTipoVittima(int tipoVittimaId, string nomeTipoVittima);
        bool CreateTipoVittima(TipoVittima tipoVittima);
        bool UpdateTipoVittima(TipoVittima tipoVittima);
        bool DeleteTipoVittima(TipoVittima tipoVittima);
        bool Save();
    }
}
