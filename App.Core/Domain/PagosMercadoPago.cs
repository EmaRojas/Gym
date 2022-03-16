using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class PagosMercadoPago : BaseEntity
    {
        public virtual DateTime Fecha { get; set; }
        public virtual Notificacion Notificacion { get; set; }
        public virtual User Usuario { get; set; }
        public virtual double Monto { get; set; }
    }
}
