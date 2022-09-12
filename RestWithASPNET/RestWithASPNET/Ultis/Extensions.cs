using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;

namespace RestWithASPNET.Ultis
{
    public static class Extensions
    {

        private static readonly Regex LastSegmentPattern =
            new Regex(@"([^:]+://[^?]+)(/[^/?#]+)(.*$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Uri ReplaceLastSegement(this Uri me, string replacement)
            => me != null ? new Uri(LastSegmentPattern.Replace(me.AbsoluteUri, $"$1/{replacement}$3")) : null;

        public static Uri RemoveLastSegement(this Uri me)
            => me != null ? new Uri(LastSegmentPattern.Replace(me.AbsoluteUri, "$1$3")) : null;

        public static Uri GetAbsoluteUri(this HttpRequest httpRequest)
        {
            var request = httpRequest;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }

    }
}
