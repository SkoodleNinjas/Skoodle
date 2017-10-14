using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Skoodle.Startup))]
namespace Skoodle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
