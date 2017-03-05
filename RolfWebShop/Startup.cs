using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RolfWebShop.Startup))]
namespace RolfWebShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
