using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: OwinStartup("AuthManagerStartUp", typeof(AuthManager.StartUp))]
namespace AuthManager
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            IdentityConfig.Configuration(app);
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
