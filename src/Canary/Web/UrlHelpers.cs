using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Canary.Web
{
    public static class UrlHelpers
    {
        private static Uri GetBaseUrl(UrlHelper url)
        {
            var req = url.RequestContext.HttpContext.Request;
            Uri contextUri = new Uri(req.Url, req.RawUrl);
            UriBuilder realmUri = new UriBuilder(contextUri)
            {
                Path = req.ApplicationPath,
                Query = null,
                Fragment = null
            };
            return realmUri.Uri;
        }

        public static string Absolute(this UrlHelper url, string relativePath)
        {
            var uriString = new Uri(GetBaseUrl(url), url.Content(relativePath)).AbsoluteUri;
            return uriString;
        }
    }
}
