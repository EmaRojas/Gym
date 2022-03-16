using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class AbonoPorCliente : BaseEntity
    {
        public virtual Cliente Cliente { get; set; }

        public virtual Abono Abono { get; set; }

        public virtual double Precio { get; set; }

        public virtual bool Pagado { get; set; }

        public virtual DateTime? FechaVencimiento { get; set; }

        public virtual DateTime? FechaIngreso { get; set; }

        public virtual bool Ficho { get; set; }

        public virtual Pago Pago { get; set; }

        public virtual string PendientePego { get; set; }

    }
}
