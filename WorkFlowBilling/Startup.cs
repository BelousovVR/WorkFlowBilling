using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkFlowBilling.Startup))]
namespace WorkFlowBilling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
