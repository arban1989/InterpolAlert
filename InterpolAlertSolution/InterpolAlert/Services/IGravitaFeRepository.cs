using System.Collections.Generic;
using InterpolAlertApi.Dtos;

namespace InterpolAlert.Services
{
    public interface IGravitaFeRepository
    {
        IEnumerable<EventoDto> GetEventiFromAGravita(int GravitaId);
        IEnumerable<GravitaDto> GetGravita();
        GravitaDto GetGravita(int GravitaId);
        GravitaDto GetGravitaOfAnEvent(int GravitaId);
    }
}