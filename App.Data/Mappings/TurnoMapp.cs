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
    public class TurnoMapp : IAutoMappingOverride<Turno>
    {
        public void Override(AutoMapping<Turno> mapping)
        {
            mapping.Table("Turnos");
        }
    }
}
