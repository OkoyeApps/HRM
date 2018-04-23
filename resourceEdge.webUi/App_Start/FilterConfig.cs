using resourceEdge.webUi.Infrastructure.Handlers;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute(),1);
            filters.Add(new ActionLoggingFilter(), 2);
            filters.Add(new LoginsFilters(), 3);
            filters.Add(new EdgeIdentityFilter(),4);
            filters.Add(new MenuFilter(), 6);
            filters.Add(new TempdateInterceptorFilter(), 5);            
        }
    }
}
