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
        bool CreateEvento(List<int> autoreId, Evento evento);
        bool UpdateEvento(List<int> autoreId, Evento evento);
        bool DeleteEvento(Evento evento);
        bool Save();
    }
}
