using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PROJECT2.Startup))]
namespace PROJECT2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
