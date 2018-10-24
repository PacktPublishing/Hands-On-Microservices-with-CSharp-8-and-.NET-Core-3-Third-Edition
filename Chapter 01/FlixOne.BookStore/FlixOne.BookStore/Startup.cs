using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlixOne.BookStore.Startup))]
namespace FlixOne.BookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
