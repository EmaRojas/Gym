using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Models
{
    public class AbonoPorClienteListModel
    {

        public int Id { get; set; }

        public string Precio { get; set; }

        public string FechaIngreso { get; set; }

        public string FechaVencimiento { get; set; }

        public string Dni { get; set; }

        public string Cliente { get; set; }

        public string Abono { get; set; }

        public string Pagado { get; set; }

        public bool Ficho { get; set; }

        public bool Vencido { get; set; }

        public string Estado { get; set; }

        public string Opciones { get; set; }

    }
}