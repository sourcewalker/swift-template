using Models.DTO;
using System;
using System.Collections.Generic;

namespace $safeprojectname$.$safeprojectname$.InstantWin.Interfaces
{
    public interface IGenerator
    {
        IList<DateTimeOffset> Generate(GeneratorConfig config);
    }
}
