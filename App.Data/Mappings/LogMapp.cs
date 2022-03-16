using App.Core.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Mappings
{
    public class LogMapp : IAutoMappingOverride<Log>
    {
        public void Override(AutoMapping<Log> mapping)
        {
            mapping.Table("Logs");
        }
    }
}
