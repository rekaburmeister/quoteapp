using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuoteApp.Startup))]
namespace QuoteApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
