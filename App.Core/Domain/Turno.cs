using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class Turno : BaseEntity
    {
        public virtual string Dia { get; set; }
        public virtual string HoraInicio { get; set; }
        public virtual string HoraFin { get; set; }
        public virtual AbonoPorCliente AbonoPorCliente { get; set; }
    }
}
