using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace $safeprojectname$
{
    public interface ISurveyService
    {
        IEnumerable<ParticipationDto> ExtractParticipation(DateTimeOffset start, DateTimeOffset end);

        int GetParticipationNumberBySite(Guid siteId);
    }
}
