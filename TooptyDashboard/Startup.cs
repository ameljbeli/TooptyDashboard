using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TooptyDashboard.Startup))]
namespace TooptyDashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
