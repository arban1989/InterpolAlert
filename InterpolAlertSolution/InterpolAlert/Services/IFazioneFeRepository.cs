using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public interface IFazioneFeRepository
    {
        IEnumerable<FazioneDto> GetFazioni();
        FazioneDto GetFazione(int fazioneId);
        FazioneDto GetFazioneByAutore(int autoreId);
        IEnumerable<AutoreDto> GetAutoriFromAFazione(int fazioneId);
        IEnumerable<MandanteDto> GetMandantiFromAFazione(int fazioneId);
    }
}
