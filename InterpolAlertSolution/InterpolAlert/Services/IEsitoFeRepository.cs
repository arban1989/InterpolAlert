﻿using InterpolAlertApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Services
{
    public interface IEsitoFeRepository
    {
        IEnumerable<EventoDto> GetEventiFromAnEsito(int esitoId);
        IEnumerable<EsitoDto> GetEsiti();
        EsitoDto GetEsito(int esitoId);
        EsitoDto GetEsitoOfAnEvent(int esitoId);
    }
}
