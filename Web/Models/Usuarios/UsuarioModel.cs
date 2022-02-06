using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models.Usuarios
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public virtual string User { get; set; }
        public virtual string NombreCompleto { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public string RepPassword { get; set; }
    }
}