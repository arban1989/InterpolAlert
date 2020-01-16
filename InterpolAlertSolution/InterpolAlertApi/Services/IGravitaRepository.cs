using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface IGravitaRepository
    {
        ICollection<Gravita> GetGravitas();
        Gravita GetGravita(int gravitaId);
        Gravita GetGravitaOfAnEvent(int gravitaId);
        ICollection<Evento> GetEventiFromAGravita(int gravitaId);
        bool GravitaExists(int gravitaId);
        bool IsDuplicateGravita(int gravitaId, string nomeGravita);
        bool CreateGravita(Gravita gravita);
        bool UpdateGravita(Gravita gravita);
        bool DeleteGravita(Gravita gravita);
        bool Save();
    }
}
