using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class RutinaPorActividad : BaseEntity
    {
        public virtual Rutina Rutina { get; set; }
        public virtual Actividad Actividad { get; set; }
        public virtual string Repeticiones { get; set; }
    }
}
