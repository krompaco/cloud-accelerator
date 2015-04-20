namespace Krompaco.CloudAccelerator
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            const string P = "Proxy";
            const string Default = "Default";
            const string CacheFarFuture = "CacheFarFuture";
            const string Cs5 = "CacheSpan5";
            const string Cs4 = "CacheSpan4";
            const string Cs3 = "CacheSpan3";
            const string Cs2 = "CacheSpan2";
            const string Cs1 = "CacheSpan1";

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Index page / root request
            routes.MapRoute("Home", string.Empty, new { controller = P, action = "Index" });

            // TODO: Add patterns and point to wanted cache config controller, some examples
            routes.MapRoute("R00", "k.nu/gui/i/{*restOfPath}", new { controller = P, action = CacheFarFuture });
            routes.MapRoute("R01", "k.nu/files/samples/{*restOfPath}", new { controller = P, action = Cs5 });

            // The catch all
            routes.MapRoute("Default", "{*url}", new { controller = P, action = Default });
        }
    }
}
