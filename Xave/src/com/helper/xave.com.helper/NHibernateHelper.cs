using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using xave.com.helper.model;

namespace xave.com.helper
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
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["log"].ConnectionString;
                    _sessionFactory = Fluently.Configure()
                        .Mappings(x => x.FluentMappings.Add(typeof(LogMap)))
                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                    .ExposeConfiguration(config =>
                    {
                        SchemaExport schemaExport = new SchemaExport(config);
                    }).BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }






        private static Dictionary<string, ISessionFactory> _sessionFactorys;
        private static Dictionary<string, ISessionFactory> SessionFactorys
        {
            get
            {
                if (_sessionFactorys == null)
                {
                    _sessionFactorys = new Dictionary<string, ISessionFactory>();
                }
                return _sessionFactorys;
            }
        }

        public static ISession GetSession(string connectionString)
        {
            if (SessionFactorys.Count() == 0 || SessionFactorys[connectionString] == null)
            {
                string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
                ISessionFactory sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString))
                .ExposeConfiguration(config => { SchemaExport schemaExport = new SchemaExport(config); }).BuildSessionFactory();

                SessionFactorys.Add(connectionString, sessionFactory);
            }
            return SessionFactorys[connectionString].OpenSession();
        }

        public static ISession GetSession(string connectionString, List<Type> types)
        {
            if (SessionFactorys.Count() == 0 || SessionFactorys[connectionString] == null)
            {
                string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
                ISessionFactory sessionFactory = Fluently.Configure()
                        .Mappings(x => types.ForEach(t => x.FluentMappings.Add(t)))
                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString))
                        .ExposeConfiguration(config =>
                        {
                            SchemaExport schemaExport = new SchemaExport(config);
                        }).BuildSessionFactory();

                SessionFactorys.Add(connectionString, sessionFactory);
            }
            return SessionFactorys[connectionString].OpenSession();
        }
    }
}
