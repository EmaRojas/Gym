using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using App.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Mappings
{
    public class NotificacionMapp : IAutoMappingOverride<Notificacion>
    {
        public void Override(AutoMapping<Notificacion> mapping)
        {
            mapping.Table("Notificaciones");
        }
    }
}
