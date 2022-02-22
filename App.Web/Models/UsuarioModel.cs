using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string FullName { get; set; }

        public string Empresa { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }

        public string RepPassword { get; set; }

        public int? EmpresaId { get; set; }

        public string Dni { get; set; }

        public string Nacimiento { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string FechaComienzo { get; set; }

        public string FechaFin { get; set; }
    }

}