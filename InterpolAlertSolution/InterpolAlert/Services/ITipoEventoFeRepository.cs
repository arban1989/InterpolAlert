using System.Collections.Generic;
using InterpolAlertApi.Dtos;

namespace InterpolAlert.Services
{
    public interface ITipoEventoFeRepository
    {
        IEnumerable<EventoDto> GetEventiFromATipoEvento(int tipoEventoId);
        IEnumerable<TipoEventoDto> GetTipiEventi();
        TipoEventoDto GetTipoEvento(int tipoEventoId);
        TipoEventoDto GetTipoTipoEventoOfAnEvent(int tipoEventoId);
    }
}