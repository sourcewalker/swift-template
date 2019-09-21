using Swift.Umbraco.Business.Manager.Interfaces;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Umbraco
{
    public class PublishedContentManager : IPublishedContentManager
    {
        private readonly ITypedPublishedContentQuery _umbracoContentQuery;

        public PublishedContentManager(ITypedPublishedContentQuery umbraContentQuery)
        {
            _umbracoContentQuery = umbraContentQuery;
        }

        public IEnumerable<IPublishedContent> GetTypedContentAtXPath(string xpath)
        {
            return _umbracoContentQuery.TypedContentAtXPath(xpath);
        }

        public IPublishedContent GetTypedContentById(int id)
        {
            return _umbracoContentQuery.TypedContent(id);
        }

        public IPublishedContent GetTypedContentSingleAtXPath(string xpath)
        {
            return _umbracoContentQuery.TypedContentSingleAtXPath(xpath);
        }

        public IPublishedContent GetTypedMediaById(int id)
        {
            return _umbracoContentQuery.TypedMedia(id);
        }

        public IEnumerable<IPublishedContent> GetTypedSearch(string term)
        {
            return _umbracoContentQuery.TypedSearch(term);
        }

        public IEnumerable<IPublishedContent> GetTypedContentAtRoot()
        {
            return _umbracoContentQuery.TypedContentAtRoot();
        }
    }
}
