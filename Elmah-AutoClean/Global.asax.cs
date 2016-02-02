using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoClean;
using Hangfire;

namespace Elmah_AutoClean
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Storage is the only thing required for basic configuration.
            // Just discover what configuration options do you have.

            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            
            //.UseActivator(...)
            //.UseLogProvider(...)
            
            RecurringJob.AddOrUpdate(() => CleanUp.AutoClean(), Cron.Daily);

        }
    }
}
