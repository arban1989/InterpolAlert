using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface IEsitoRepository
    {
        ICollection<Esito> GetEsiti();
        Esito GetEsito(int esitoId);
        Esito GetEsitoOfAnEvent(int eventoId);
        ICollection<Evento> GetEventiFromAnEsito(int esitoId);
        bool EsitoExists(int esitoId);
        bool IsDuplicateEsito(int esitoId, string nomeEsito);
        bool CreateEsito(Esito esito);
        bool UpdateEsito(Esito esito);
        bool DeleteEsito(Esito esito);
        bool Save();
    }
}
