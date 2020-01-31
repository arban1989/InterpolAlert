using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpolAlertApi.Dtos;

namespace InterpolAlert.Services
{
    public interface ILocalitaFeRepository
    {
        IEnumerable<LocalitaDto> GetLocalitas();
        LocalitaDto GetLocalita(int localitaId);
        LocalitaDto GetLocalitaOfAnEvent(int eventoId);
        IEnumerable<EventoDto> GetEventiFromALocalita(int localitaId);
    }
}
