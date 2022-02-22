using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public  class FichaCliente : BaseEntity
    {
        public virtual Cliente Cliente { get; set; }
        public virtual bool Activo { get; set; }
        public virtual string Edad { get; set; }
        public virtual string Medico { get; set; }
        public virtual string Altura { get; set; }
        public virtual string Peso { get; set; }
        public virtual string GrupoSanguineo { get; set; }
        public virtual bool PColumna { get; set; }
        public virtual string DetalleColumna { get; set; }
        public virtual bool ECardiaca { get; set; }
        public virtual string DetalleCardiaca { get; set; }
        public virtual bool LRecientes { get; set; }
        public virtual string DetalleLesion { get; set; }
        public virtual bool Deportes { get; set; }
        public virtual string DetalleDeporte { get; set; }
        public virtual string Observacion { get; set; }
        public virtual string ObjPersonal { get; set; }
    }
}
