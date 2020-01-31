using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface IEventoRepository
    {
        ICollection<Evento> GetEventi();
        Evento GetEvento(int eventoId);
        bool EventoExists(int eventoId);
        bool IsDuplicateEvent(int eventoId, string nomeEvento);
        bool CreateEvento(List<int> listaAutoriId, int tipoVittimaId, int localitaId, int gravitaId, int esitoId, int tipoEventoId, int mandanteId,  Evento evento);
        bool UpdateEvento(List<int> listaAutoriId, int tipoVittimaId, int localitaId, int gravitaId, int esitoId, int tipoEventoId, int mandanteId, Evento evento);
        bool DeleteEvento(Evento evento);
        bool Save();
    }
}
