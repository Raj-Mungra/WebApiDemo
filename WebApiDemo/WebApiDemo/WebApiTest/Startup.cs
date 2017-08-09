using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApiTest.Startup))]
namespace WebApiTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
