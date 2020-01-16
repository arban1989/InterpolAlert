using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface IMandanteRepository
    {
        ICollection<Mandante> GetMandanti();
        Mandante GetMandante(int mandanteId);
        Mandante GetMandanteOfAnEvent(int eventoId);
        Fazione GetFazioneByMandante(int mandanteId);
        ICollection<Evento> GetEventiFromAMandante(int mandanteId);
        bool MandanteExists(int mandanteId);
        bool IsDuplicateMandante(int mandanteId, string nomeMandante);
        bool CreateMandante(Mandante mandante);
        bool UpdateMandante(Mandante mandante);
        bool DeleteMandante(Mandante mandante);
        bool Save();
    }
}
