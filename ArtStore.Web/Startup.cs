using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(ArtStore.Web.Startup))]
namespace ArtStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
