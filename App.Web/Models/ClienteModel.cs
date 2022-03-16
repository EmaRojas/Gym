using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Models
{
    public class ClienteModel
    {
        public ClienteModel()
        {
            ListaCentros = new List<SelectListItem>();
        }
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Email { get; set; }

        public string FechaNacimiento { get; set; }

        public bool TieneAbono { get; set; }

        public List<SelectListItem> ListaCentros { get; set; }
        public int CentroId { get; set; }
        public string CentoSalud { get; set; }

    }
}