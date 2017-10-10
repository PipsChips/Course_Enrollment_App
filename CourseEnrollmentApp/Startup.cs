using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseEnrollmentApp.Startup))]
namespace CourseEnrollmentApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
