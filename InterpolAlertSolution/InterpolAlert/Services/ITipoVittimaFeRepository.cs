using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public interface ITipoVittimaFeRepository
    {
        IEnumerable<TipoVittimaDto> GetTipiVittima();
        TipoVittimaDto GetTipoVittima(int tipoVittimaId);
        TipoVittimaDto GetTipoVittimaOfAnEvent(int eventoId);
        IEnumerable<EventoDto> GetEventiFromATipoVittima(int tipoVittimaId);
    }
}
