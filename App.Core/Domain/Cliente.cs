using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class Cliente : BaseEntity
    {
        public virtual string Nombre { get; set; }

        public virtual string Apellido { get; set; }

        public virtual string Dni { get; set; }

        public virtual string Telefono { get; set; }

        public virtual string Direccion { get; set; }

        public virtual string Email { get; set; }

        public virtual string FechaNacimiento { get; set; }

    }
}
