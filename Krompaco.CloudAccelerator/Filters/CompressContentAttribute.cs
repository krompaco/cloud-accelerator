namespace Krompaco.CloudAccelerator.Filters
{
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Thank you: http://weblog.west-wind.com/posts/2012/Apr/28/GZipDeflate-Compression-in-ASPNET-MVC
    /// </summary>
    public class CompressContentAttribute : ActionFilterAttribute
    {
        public static bool IsGZipSupported()
        {
            string acceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            if (!string.IsNullOrEmpty(acceptEncoding)
                && (acceptEncoding.Contains("gzip") || acceptEncoding.Contains("deflate")))
            {
                return true;
            }

            return false;
        }

        public static void GZipEncodePage()
        {
            var response = HttpContext.Current.Response;

            if (IsGZipSupported())
            {
                string acceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

                if (acceptEncoding.Contains("gzip"))
                {
                    response.Filter = new System.IO.Compression.GZipStream(
                                                response.Filter, System.IO.Compression.CompressionLevel.Optimal);
                    response.Headers.Remove("Content-Encoding");
                    response.AppendHeader("Content-Encoding", "gzip");
                }
                else
                {
                    response.Filter = new System.IO.Compression.DeflateStream(
                                                response.Filter, System.IO.Compression.CompressionLevel.Optimal);
                    response.Headers.Remove("Content-Encoding");
                    response.AppendHeader("Content-Encoding", "deflate");
                }
            }

            response.AppendHeader("Vary", "Content-Encoding");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GZipEncodePage();
        }
    }
}