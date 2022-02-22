using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class Abono : BaseEntity
    {
        public virtual string Nombre { get; set; }
        public virtual string Precio { get; set; }
    }
}
