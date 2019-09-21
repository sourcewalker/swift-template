using Swift.Umbraco.Business.Manager.Interfaces;
using Umbraco.Core.Services;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Umbraco
{
    public class MediaServiceManager : IMediaServiceManager
    {
        private readonly IMediaService _mediaService;

        public MediaServiceManager(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }
    }
}
