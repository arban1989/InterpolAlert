using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public interface IAutoreFeRepository
    {
        IEnumerable<AutoreDto> GetAutori();
        AutoreDto GetAutore(int autoreId);
        IEnumerable<EventoDto> GetEventiOfAnAutore(int autoreId);
        IEnumerable<AutoreDto> GetAutoriFromAnEvent(int eventoId);
        FazioneDto GetFazioneOfAnAutore(int autoreId);
    }
}
