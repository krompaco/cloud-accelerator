namespace Krompaco.CloudAccelerator.Controllers
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;

    using Krompaco.CloudAccelerator.Filters;
    using Krompaco.CloudAccelerator.Results;

    public class ProxyController : Controller
    {
        private const string H = "Content-Encoding";
        private const string P = "*";

        [CompressContent]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Any, VaryByParam = P, VaryByHeader = H)]
        public HttpWebResponseResult CacheSpan5()
        {
            return GetByIncomingRequest(Request);
        }

        [CompressContent]
        [OutputCache(Duration = 30, Location = OutputCacheLocation.Any, VaryByParam = P, VaryByHeader = H)]
        public HttpWebResponseResult CacheSpan4()
        {
            return GetByIncomingRequest(Request);
        }

        [CompressContent]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Any, VaryByParam = P, VaryByHeader = H)]
        public HttpWebResponseResult CacheSpan3()
        {
            return GetByIncomingRequest(Request);
        }

        [CompressContent]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Any, VaryByParam = P, VaryByHeader = H)]
        public HttpWebResponseResult CacheSpan2()
        {
            return GetByIncomingRequest(Request);
        }

        [CompressContent]
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Any, VaryByParam = P, VaryByHeader = H)]
        public HttpWebResponseResult CacheSpan1()
        {
            return GetByIncomingRequest(Request);
        }

        // No compression and far future expiration, for JPG files and similar
        [OutputCache(Duration = 31536000, Location = OutputCacheLocation.Any, VaryByParam = P)]
        public HttpWebResponseResult CacheFarFuture()
        {
            return GetByIncomingRequest(Request);
        }

        // Anything but index / root page
        [OutputCache(Duration = 30, Location = OutputCacheLocation.Any, VaryByParam = P, VaryByHeader = H)]
        public HttpWebResponseResult Default()
        {
            return GetByIncomingRequest(Request);
        }

        // Root page
        [OutputCache(Duration = 2, Location = OutputCacheLocation.Any, VaryByParam = P, VaryByHeader = H)]
        public ActionResult Index()
        {
            return View();
        }

        private static HttpWebResponseResult GetByIncomingRequest(HttpRequestBase r)
        {
            if (r.Url != null)
            {
                string urlAsString = string.Concat("http:/", r.Url.PathAndQuery);

                var uri = new Uri(urlAsString);

                // TODO: Add the domain names you allow to fetch from
                var validHosts = new[]
                                     {
                                         "k.nu",
                                         "krompaco.nu"
                                     };

                if (!validHosts.Contains(uri.Host.ToLower()))
                {
                    return null;
                }

                // An example of URL shortening
                if (uri.Host.Equals("k.nu", StringComparison.OrdinalIgnoreCase))
                {
                    urlAsString = urlAsString.Replace(
                        "http://k.nu",
                        "http://krompaco.nu");
                }

                var request = (HttpWebRequest)WebRequest.Create(urlAsString);

                request.Timeout = int.Parse(ConfigurationManager.AppSettings.Get("WebRequestTimeout"));

                var getResponse = (HttpWebResponse)request.GetResponse();

                return new HttpWebResponseResult(getResponse);
            }

            return null;
        }
    }
}