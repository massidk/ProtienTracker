using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Funq;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.Redis;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.OrmLite.SqlServer;
using System.Web.Configuration;
using ProteinTracker.Api;
using ServiceStack.Configuration;

namespace ProteinTracker
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new ProteinTrackerAppHost().Init();
        }
    }

    public class ProteinTrackerAppHost : AppHostBase
    {
        public ProteinTrackerAppHost() : base("Protein Tracker Web Services", typeof(UserService).Assembly) {}

        public override void Configure(Funq.Container container)
        {
            SetConfig(new EndpointHostConfig { ServiceStackHandlerFactoryPath = "api" });

            var factory = new RepositoryFactory();
            IRepository repo = factory.GetRepository(WebConfigurationManager.AppSettings["dbo"]);
            //container.Register<IRedisClientsManager>(c => new PooledRedisClientManager());
            //container.Register<IRepository>(c => new RedisRepository(c.Resolve<IRedisClientsManager>()));
            container.Register(repo);
            //container.RegisterAutoWired<SqlRepository>();
            //container.RegisterAutoWiredAs<repo, IRepository>();
        }
    }
}