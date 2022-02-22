using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class Rutina : BaseEntity
    {
        public virtual Cliente Cliente { get; set; }
        public virtual Dia Dia  { get; set; }
    }
}
