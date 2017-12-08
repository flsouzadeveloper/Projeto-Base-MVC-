using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EstudoArchictetureDDDVS2015.Startup))]
namespace EstudoArchictetureDDDVS2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
