using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieRentalProject.Startup))]
namespace MovieRentalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
