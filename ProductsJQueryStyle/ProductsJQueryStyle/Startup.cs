using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductsJQueryStyle.Startup))]
namespace ProductsJQueryStyle
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
