using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyAzureDatabase_OData.Startup))]
namespace MyAzureDatabase_OData
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
