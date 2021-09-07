using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Cache;
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
                var configuration = new Configuration().Configure(Configuration.DefaultHibernateCfgFileName);
                _sessionFactory = Fluently.Configure(configuration)
                    .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load("ExpenseTracker.Infrastructure")))
                    .Cache(
                        c => c.UseQueryCache()
                            .UseSecondLevelCache()
                            .ProviderClass<HashtableCacheProvider>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Create(true, true);
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