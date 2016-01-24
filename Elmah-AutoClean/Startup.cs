using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Elmah_AutoClean.Startup))]
namespace Elmah_AutoClean
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
