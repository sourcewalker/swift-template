using Swift.Umbraco.Business.Manager.Interfaces;
using Umbraco.Core.Services;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Umbraco
{
    public class ContentServiceManager : IContentServiceManager
    {
        private readonly IContentService _contentService;

        public ContentServiceManager(IContentService contentService)
        {
            _contentService = contentService;
        }
    }
}
