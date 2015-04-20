namespace Krompaco.CloudAccelerator.Results
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Web.Mvc;

    public class HttpWebResponseResult : ActionResult
    {
        private readonly HttpWebResponse response;

        private readonly ActionResult innerResult;

        public HttpWebResponseResult(HttpWebResponse responseToRelay)
        {
            if (responseToRelay == null)
            {
                throw new ArgumentNullException("responseToRelay");
            }

            response = responseToRelay;

            Stream contentStream;

            if (responseToRelay.ContentEncoding.Contains("gzip"))
            {
                contentStream = new GZipStream(responseToRelay.GetResponseStream(), CompressionMode.Decompress);
            }
            else if (responseToRelay.ContentEncoding.Contains("deflate"))
            {
                contentStream = new DeflateStream(responseToRelay.GetResponseStream(), CompressionMode.Decompress);
            }
            else
            {
                contentStream = responseToRelay.GetResponseStream();
            }

            if (string.IsNullOrEmpty(responseToRelay.CharacterSet))
            {
                // File result
                innerResult = new FileStreamResult(contentStream, responseToRelay.ContentType);
            }
            else
            {
                // Text result
                var contentResult = new ContentResult { Content = new StreamReader(contentStream).ReadToEnd() };
                innerResult = contentResult;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var clientResponse = context.HttpContext.Response;
            clientResponse.StatusCode = (int)response.StatusCode;

            foreach (var headerKey in response.Headers.AllKeys)
            {
                switch (headerKey)
                {
                    case "Content-Length":
                    case "Transfer-Encoding":
                    case "Content-Encoding":
                    case "Pragma":
                    case "Cache-Control":
                    case "Expires":
                    case "X-AspNet-Version":
                    case "Vary":
                    case "Date":
                    case "Last-Modified":
                    case "ETag":
                    case "If-Modified-Since":
                    case "Age":
                    case "Accept-Ranges":
                    case "Server":
                    case "Set-Cookie":
                    case "X-AspNetMvc-Version":
                    case "X-Powered-By":
                    case "X-UA-Compatible":
                        // Handled either by IIS, by OutputCache directives or unwanted
                        break;

                    default:
                        clientResponse.AddHeader(headerKey, response.Headers[headerKey]);
                        break;
                }
            }

            innerResult.ExecuteResult(context);
        }
    }
}