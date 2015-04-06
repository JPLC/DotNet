using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrelloMVC.Startup))]
namespace TrelloMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
