using Microsoft.Owin;
using Owin;
using resourceEdge.webUi.Infrastructure;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(resourceEdge.webUi.Startup))]
namespace resourceEdge.webUi
{
    public partial class Startup
    {
      //  public resourceEdge.Domain.Entities.EdgeDbContext db = new Domain.Entities.EdgeDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDeafaultRoles();           
        }
        
        public void CreateDeafaultRoles()
        {
            Rolemanager manager = new Rolemanager();
            manager.CreateRoles();
        }
    }
}
