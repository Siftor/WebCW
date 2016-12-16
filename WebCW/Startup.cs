using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCW.Startup))]
namespace WebCW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
