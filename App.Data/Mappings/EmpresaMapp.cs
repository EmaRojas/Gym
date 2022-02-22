using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using App.Core;
using App.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Mappings
{
    public class EmpresaMapp : IAutoMappingOverride<Empresa>
    {
        public void Override(AutoMapping<Empresa> mapping)
        {
            mapping.Table("Empresas");
        }
    }
}