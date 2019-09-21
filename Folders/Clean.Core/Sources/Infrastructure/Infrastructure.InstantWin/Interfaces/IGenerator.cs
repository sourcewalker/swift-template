using Core.Infrastructure.Interfaces.InstantWin;
using System;
using System.Collections.Generic;

namespace $safeprojectname$.Interfaces
{
    public interface IGenerator
    {
        IList<DateTimeOffset> Generate(GeneratorConfig config);
    }
}
