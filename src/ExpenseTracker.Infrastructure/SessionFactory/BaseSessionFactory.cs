using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;
using ISession = NHibernate.ISession;

namespace ExpenseTracker.Infrastructure.SessionFactory
{
    public static class BaseSessionFactory
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory _sessionFactory;
        private static readonly object LockObject = new();

        public static IHttpContextAccessor? HttpContextAccessor { get; set; }

        static BaseSessionFactory()
        {
            lock (LockObject)
            {
                if (_sessionFactory != null) return;
                _sessionFactory = BuildSessionFactory(Configuration.DefaultHibernateCfgFileName,true);
            }
        }


        private static ISessionFactory BuildSessionFactory(string connectionStringName, bool hasToCreateSchema = false)
        {
            return Fluently.Configure(new Configuration().Configure(connectionStringName))
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssembly(Assembly.Load(nameof(ExpenseTracker.Infrastructure)));
                    m.UsePersistenceModel(new PersistenceModel());
                })
                .ExposeConfiguration(cfg => BuildSchema(cfg, hasToCreateSchema))
                .BuildSessionFactory();
        }


        /// <summary>  
        /// Build the schema of the database.  
        /// </summary>  
        /// <param name="config">Configuration.</param>
        /// <param name="hasToCreateSchema"></param>
        private static void BuildSchema(Configuration config, bool hasToCreateSchema)
        {
            if (hasToCreateSchema)
                new SchemaExport(config).Create(false, true);
            else
                new SchemaUpdate(config).Execute(false, false);
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Create(false, true);
        }

        public static ISession GetCurrentSession()
        {
            var context = HttpContextAccessor.HttpContext;

            if (context.Items[CurrentSessionKey] is ISession currentSession) return currentSession;
            currentSession = _sessionFactory.OpenSession();
            context.Items[CurrentSessionKey] = currentSession;

            return currentSession;
        }
    }
}