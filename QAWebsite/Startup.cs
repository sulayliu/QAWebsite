using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QAWebsite.Startup))]
namespace QAWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
