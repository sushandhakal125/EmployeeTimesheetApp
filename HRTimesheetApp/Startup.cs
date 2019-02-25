using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRTimesheetApp.Startup))]
namespace HRTimesheetApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
