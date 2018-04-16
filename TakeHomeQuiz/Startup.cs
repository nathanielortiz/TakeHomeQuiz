using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TakeHomeQuiz.Startup))]
namespace TakeHomeQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
