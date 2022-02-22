using App.Core.Common;
using FluentNHibernate.Automapping;
using System;

namespace App.Data
{
    /// <summary>
    /// Configuracion de AutoMapping donde se identifica que clase se debe mapear
    /// </summary>
    public class AutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.IsSubclassOf(typeof(BaseEntity));
        }
    }
}
