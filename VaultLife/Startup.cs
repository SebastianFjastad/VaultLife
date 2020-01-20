using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vaultlife.Startup))]
namespace Vaultlife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
