namespace Krompaco.CloudAccelerator
{
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Indications that this might be a problem have been found, more info:
            // https://social.msdn.microsoft.com/Forums/azure/en-US/d84ba34b-b0e0-4961-a167-bbe7618beb83/net-and-adonet-data-service-performance-tips-for-windows-azure-tables?forum=windowsazuredata
            ServicePointManager.DefaultConnectionLimit = 512;

            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
