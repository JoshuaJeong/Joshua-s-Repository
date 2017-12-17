using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateLog4Net
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    string connString = System.Configuration.ConfigurationManager.ConnectionStrings["log"].ConnectionString;

                    _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2005
                    .ConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings["log"].ConnectionString))
										.Mappings(m => m.FluentMappings.AddFromAssemblyOf<StoreMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMap>())
                    .ExposeConfiguration(config =>
                    {
                        SchemaExport schemaExport = new SchemaExport(config);
                    })
                    .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
