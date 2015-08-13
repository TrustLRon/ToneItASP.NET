using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestLog.Startup))]
namespace TestLog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
