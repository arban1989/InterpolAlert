using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public interface IMandanteFeRepository
    {
        IEnumerable<MandanteDto> GetMandanti();
        MandanteDto GetMandante(int mandanteId);
        IEnumerable<EventoDto> GetEventiOfAMandante(int mandanteId);
        MandanteDto GetMandanteOfAnEvent(int eventoId);
        FazioneDto GetFazioneOfAMandante(int mandanteId);
    }
}
