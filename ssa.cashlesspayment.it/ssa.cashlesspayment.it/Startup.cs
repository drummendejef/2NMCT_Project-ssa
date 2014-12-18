using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ssa.cashlesspayment.it.Startup))]
namespace ssa.cashlesspayment.it
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
