using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StatTracker.Web.MVC.Startup))]
namespace StatTracker.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
