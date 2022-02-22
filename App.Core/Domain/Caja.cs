using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class Caja : BaseEntity
    {
        public virtual DateTime? FechaApertura { get; set; }

        public virtual DateTime? FechaCierre { get; set; }

        public virtual double MontoApertura { get; set; }

        public virtual double Total { get; set; }

        public virtual bool Estado { get; set; }

        public virtual int Numero { get; set; }

        public virtual IEnumerable<Pago> Pago { get; set; }
    }
}
