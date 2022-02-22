using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class Pago : BaseEntity
    {
        public virtual DateTime Fecha { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual string Descripcion { get; set; }

        //Ingreso 1 - Egreso 2
        public virtual int Tipo { get; set; }

        public virtual Caja Caja { get; set; }

        public virtual double Monto { get; set; }
    }
}
