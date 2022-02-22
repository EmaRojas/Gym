using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;

namespace App.Data
{
    /// <summary>
    /// Configuracion customizada para la creacion tabla intermedia para relaciones
    /// </summary>
    public class CustomManyToManyConvention : ManyToManyTableNameConvention
    {
        protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
        {
            return collection.EntityType.Name + "" + otherSide.EntityType.Name;
        }

        protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
        {
            return collection.EntityType.Name + "" + collection.ChildType.Name;
        }
    }
}
