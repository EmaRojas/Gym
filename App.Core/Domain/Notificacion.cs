using App.Core.Common;
using System;

namespace App.Core.Domain
{
    public class Notificacion : BaseEntity
    {
        public virtual long IdNotificacionMP { get; set; }
        public virtual string Topic { get; set; }
        public virtual DateTime Fecha { get; set; }
    }
}