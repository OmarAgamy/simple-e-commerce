using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhaseOne.Startup))]
namespace PhaseOne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
