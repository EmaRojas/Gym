using App.Core.Common;
using App.Core.Infrastructure;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace App.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory SessionFactory;
        public ITransaction Transaction;
        public readonly ISession Session;

        static UnitOfWork()
        {
            SessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(x => x.FromConnectionStringWithKey("DefaultConnection")))
                .Mappings(a => a.AutoMappings.Add(AutoMap.AssemblyOf<BaseEntity>()
                    .Conventions.Add<CustomForeignKeyConvention>()
                    .Conventions.Add<CustomManyToManyConvention>()
                    .UseOverridesFromAssemblyOf<AutoMappingConfiguration>()
                    .IgnoreBase(typeof(BaseEntity))))
                .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
                .BuildSessionFactory();
        }

        public UnitOfWork()
        {
            Session = SessionFactory.OpenSession();
            //Transaction = Session.BeginTransaction();
        }

        public void BeginTransaction()
        {
            Transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (Transaction.IsActive)
                {
                    Transaction.Commit();
                }
            }
            catch
            {
                Transaction.Rollback();

                Session.Dispose();
                Transaction.Dispose();

                throw;
            }
        }
    }
}
