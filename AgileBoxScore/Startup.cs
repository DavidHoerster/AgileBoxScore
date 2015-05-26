using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgileBoxScore.Startup))]
namespace AgileBoxScore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //GlobalHost.DependencyResolver.UseRedis("localhost", 6379, "agileways", "AgileSignalR");
            app.MapSignalR();   
        }
    }
}