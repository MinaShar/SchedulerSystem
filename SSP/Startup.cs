using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSP.Startup))]
namespace SSP
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
