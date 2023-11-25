using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(T2NSHOP.Startup))]
namespace T2NSHOP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
