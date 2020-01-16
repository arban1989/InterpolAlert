using InterpolAlertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlertApi.Services
{
    public interface IFazioneRepository
    {
        ICollection<Fazione> GetFazioni();
        Fazione GetFazione(int fazioneId);
        Fazione GetFazioneByAuthor(int authorId);
        ICollection<Autore> GetAutoriFromAFazione(int fazioneId);
        ICollection<Mandante> GetMandantiFromAFazione(int fazioneId);
        bool FazioneExists(int fazioneId);
        bool IsDuplicateFazione(int fazioneId, string nomeFazione);
        bool CreateFazione(Fazione fazione);
        bool UpdateFazione(Fazione fazione);
        bool DeleteFazione(Fazione fazione);
        bool Save();
    }
}
