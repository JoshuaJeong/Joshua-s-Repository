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






        private static Dictionary<string, ISession> _sessions;
        private static Dictionary<string, ISession> Sessions
        {
            get
            {
                if (_sessions == null)
                {
                    _sessions = new Dictionary<string, ISession>();
                }
                return _sessions;
            }
        }

        public static ISession GetSession(string name, List<Type> types)
        {
            try
            {
                if (Sessions.Count() == 0 || Sessions[name] == null)
                {
                    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
                    ISessionFactory sessionFactory = Fluently.Configure()
                            .Mappings(x => types.ForEach(t => x.FluentMappings.Add(t)))
                            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString))
                            .ExposeConfiguration(config =>
                            {
                                SchemaExport schemaExport = new SchemaExport(config);
                            }).BuildSessionFactory();

                    Sessions.Add(name, sessionFactory.OpenSession());
                }
                return Sessions[name];
            }
            catch
            {
                throw;
            }
        }
    }
}
