using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TooptyClient.Startup))]
namespace TooptyClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
