using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface ILocalitaRepository
    {
        ICollection<Localita> GetLocalitas();
        Localita GetLocalita(int localitaId);
        Localita GetLocalitaOfAnEvent(int eventoId);
        ICollection<Evento> GetEventiFromALocalita(int localitaId);
        bool LocalitaExists(int localitaId);
        bool IsDuplicateLocalita(int localitaId, string nomeLocalita);
        bool CreateLocalita(Localita localita);
        bool UpdateLocalita(Localita localita);
        bool DeleteLocalita(Localita localita);
        bool Save();
    }
}
