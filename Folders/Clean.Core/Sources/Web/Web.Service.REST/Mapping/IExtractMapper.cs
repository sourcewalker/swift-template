using Core.Shared.DTO;
using System.Collections.Generic;
using $safeprojectname$.Models;

namespace $safeprojectname$.Mapping
{
    public interface IExtractMapper
    {
        ExtractModel toExtractModel(ParticipationDto participation);

        IEnumerable<ExtractModel> toExtractModels(IEnumerable<ParticipationDto> participations);
    }
}
