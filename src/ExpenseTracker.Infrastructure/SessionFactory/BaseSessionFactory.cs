// using System.Reflection;
// using ExpenseTracker.Infrastructure.Mapping;
// using FluentNHibernate;
// using FluentNHibernate.Cfg;
// using Microsoft.AspNetCore.Http;
// using NHibernate;
// using NHibernate.Cache;
// using NHibernate.Tool.hbm2ddl;
// using Configuration = NHibernate.Cfg.Configuration;
// using ISession = NHibernate.ISession;

// namespace ExpenseTracker.Infrastructure.SessionFactory
// {
//     public static class BaseSessionFactory
//     {
//         private const string CurrentSessionKey = "nhibernate.current_session";
//         private static readonly ISessionFactory _sessionFactory;
//         private static readonly object LockObject = new();

//         public static IHttpContextAccessor? HttpContextAccessor { get; set; }

//         static BaseSessionFactory()
//         {
//             return;
//             var connectionString = "Server=localhost; port=5432; Username=postgres; Password=admin; Database=expense_tracker";
//             lock (LockObject)
//             {
//                 if (_sessionFactory != null) return;
//                 var configuration = new Configuration().Configure(Configuration.DefaultHibernateCfgFileName);
//                 _sessionFactory = Fluently.Configure()
//                 .Database(
//                     FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
//                     .Mappings(m => {
//                         m.FluentMappings.AddFromAssembly(Assembly.Load("ExpenseTracker.Infrastructure"));
//                         m.FluentMappings.Add<UserMap>();
//                     })
//                     .Cache(
//                         c => c.UseQueryCache()
//                             .UseSecondLevelCache()
//                             .ProviderClass<HashtableCacheProvider>())
//                     .ExposeConfiguration(BuildSchema)
//                     .BuildSessionFactory();
//             }
//         }

//         private static void BuildSchema(Configuration config)
//         {
//             // new SchemaExport(config)
//             //     .Create(true, false);
//             var update = new SchemaUpdate(config);
//             update.Execute(false, true);
//         }

//         public static ISession GetCurrentSession()
//         {
//             var context = HttpContextAccessor.HttpContext;

//             if (context.Items[CurrentSessionKey] is ISession currentSession) return currentSession;
//             currentSession = _sessionFactory.OpenSession();
//             context.Items[CurrentSessionKey] = currentSession;

//             return currentSession;
//         }
//     }
// }