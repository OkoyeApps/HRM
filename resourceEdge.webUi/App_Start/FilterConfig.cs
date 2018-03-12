using resourceEdge.webUi.Infrastructure.Handlers;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute(),2);
            filters.Add(new EdgeIdentityFilter(),1);
            filters.Add(new MenuFilter(), 3);
            
        }
    }
}
