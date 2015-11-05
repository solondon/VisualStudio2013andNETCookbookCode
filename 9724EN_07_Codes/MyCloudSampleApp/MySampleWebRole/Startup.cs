using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySampleWebRole.Startup))]
namespace MySampleWebRole
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
