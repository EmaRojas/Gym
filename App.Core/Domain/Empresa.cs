using App.Core.Common;
using App.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class Empresa : BaseEntity
    {
        public virtual string Nombre { get; set; }
        public virtual string Telefono { get; set; }
        public virtual User Usuario { get; set; }
        public virtual bool Tarjeta { get; set; }
        public virtual string Descripcion { get; set; }
    }
}
