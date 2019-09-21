using System;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Swift.Umbraco.$safeprojectname$.Helper
{
    public static class PublishedContentExtensions
    {
        public static string GetPath(this IPublishedContent content)
        {
            var contentUrlName = content.UrlName;
            return string.IsNullOrEmpty(contentUrlName) ? content.Url : $"/{contentUrlName}";
        }

        public static string GetUrl(this IPublishedContent content)
        {
            var absoluteUrl = new Uri(content.UrlAbsolute());
            var domain = absoluteUrl.GetLeftPart(UriPartial.Authority);
            return $"{domain}{content.GetPath()}";
        }
    }
}
