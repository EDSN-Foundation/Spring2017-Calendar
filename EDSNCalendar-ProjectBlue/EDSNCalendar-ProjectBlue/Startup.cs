using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EDSNCalendar_ProjectBlue.Startup))]
namespace EDSNCalendar_ProjectBlue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
