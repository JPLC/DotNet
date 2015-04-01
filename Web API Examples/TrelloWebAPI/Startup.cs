using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TrelloWebAPI.Startup))]

namespace TrelloWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}