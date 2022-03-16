using App.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Models
{
    public class AbonoPorClienteModel
    {
        public AbonoPorClienteModel()
        {
            ListaCliente = new List<SelectListItem>();
            ListaAbono = new List<SelectListItem>();
        }
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int AbonoId { get; set; }

        public string FechaVencimiento { get; set; }

        public string FechaIngreso { get; set; }

        public string Precio { get; set; }

        public bool Pagado { get; set; }

        public bool Ficho { get; set; }

        public bool Caja { get; set; }

        public string PendientePago { get; set; }

        public List<SelectListItem> ListaCliente { get; set; }

        public List<SelectListItem> ListaAbono { get; set; }
    }
}