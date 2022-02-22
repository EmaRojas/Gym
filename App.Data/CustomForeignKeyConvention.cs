using FluentNHibernate;
using FluentNHibernate.Conventions;
using System;

namespace App.Data
{
    /// <summary>
    /// Configuracion customizada para la creacion de claves foreaneas para relaciones
    /// </summary>
    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
            {
                return type.Name + "Id";
            }

            return property.Name + "Id";
        }
    }
}
