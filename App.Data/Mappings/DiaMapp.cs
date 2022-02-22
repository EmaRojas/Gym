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
    public class DiaMapp : IAutoMappingOverride<Dia>
    {
        public void Override(AutoMapping<Dia> mapping)
        {
            mapping.Table("Dias");
        }
    }
}