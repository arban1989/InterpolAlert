using InterpolAlertApi.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolAlert.Components
{
    public class ListaAutori
    {
        private List<AutoreDto> listaAutori = new List<AutoreDto>();

        public ListaAutori(List<AutoreDto> listaAutori)
        {
            this.listaAutori = listaAutori;
        }

        public List<SelectListItem> GetListaAutori()
        {
            var items = new List<SelectListItem>();
            foreach (var autore in listaAutori)
            {
                items.Add(new SelectListItem
                {
                    Text = autore.NomeAutore,
                    Value = autore.AutoreId.ToString(),
                    Selected = false
                });
            }

            return items;
        }

        public List<SelectListItem> GetListaAutori(List<int> autoriSelezionati)
        {
            var items = new List<SelectListItem>();
            foreach (var autore in listaAutori)
            {
                items.Add(new SelectListItem
                {
                    Text = autore.NomeAutore,
                    Value = autore.AutoreId.ToString(),
                    Selected = autoriSelezionati.Contains(autore.AutoreId) ? true : false
                });
            }

            return items;
        }
    }
}
