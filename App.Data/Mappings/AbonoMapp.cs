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
    public class AbonoMapp : IAutoMappingOverride<Abono>
    {
        public void Override(AutoMapping<Abono> mapping)
        {
            mapping.Table("Abonos");
        }
    }
}
