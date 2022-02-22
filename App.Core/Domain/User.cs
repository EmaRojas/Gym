using App.Core.Common;
using System;

namespace App.Core.Domain
{
    public class User : BaseEntity
    {
        public virtual string UserName { get; set; }

        public virtual string FullName { get; set; }

        public virtual string Email { get; set; }

        public virtual string Role { get; set; }

        public virtual string Password { get; set; }

        public virtual int EmpresaId { get; set; }

        public virtual string Dni { get; set; }

        public virtual string Direccion { get; set; }

        public virtual string Telefono { get; set; }

        public virtual DateTime FechaVencimiento { get; set; }

        public virtual DateTime FechaComienzo { get; set; }

    }
}
