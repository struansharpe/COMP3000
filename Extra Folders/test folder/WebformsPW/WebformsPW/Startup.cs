using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebformsPW.Startup))]
namespace WebformsPW
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
