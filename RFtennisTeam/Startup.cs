using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RFtennisTeam.Startup))]
namespace RFtennisTeam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
