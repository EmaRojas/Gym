using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Models
{
    public class SuscripcionModel
    {
        public string NombreCompleto { get; set; }

        public string FechaVencimiento { get; set; }

        public string FechaInicio { get; set; }

        public string Mensual { get; set; }

        public string Semestral { get; set; }

        public string Anual { get; set; }
    }
}