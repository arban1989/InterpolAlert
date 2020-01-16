using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface IAutoreRepository
    {
        ICollection<Autore> GetAutori();
        Autore GetAutore(int autoreId);
        Fazione GetFazioneByAutore(int autoreId);
        ICollection<Autore> GetAllAutoriFromAnEvent(int eventoId);
        ICollection<Evento> GetAllEventiFromAnAutore(int autoreId);
        bool AutoreExists(int autoreId);
        bool IsDuplicateAutoreName(int autoreId, string nomeAutore);
        bool CreateAutore(Autore autore);
        bool UpdateAutore(Autore autore);
        bool DeleteAutore(Autore autore);
        bool Save();
    }
}
