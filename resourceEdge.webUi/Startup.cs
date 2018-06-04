using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using resourceEdge.webUi.Infrastructure;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(resourceEdge.webUi.Startup))]
namespace resourceEdge.webUi
{
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDeafaultRoles();
            //app.MapSignalR();
            var options = new SqlServerStorageOptions() { PrepareSchemaIfNecessary = true };
            GlobalConfiguration.Configuration
              .UseSqlServerStorage("ResourceDbContext", options);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }

        public void CreateDeafaultRoles()
        {
            Rolemanager manager = new Rolemanager();
            manager.CreateRoles();
        }
    }
}
