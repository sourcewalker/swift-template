using $safeprojectname$.Enums;
using $safeprojectname$.Generator.Algorithms;
using $safeprojectname$.Interfaces;

namespace $safeprojectname$.Generator.Factory
{
    public static class GeneratorFactory
    {
        public static IGenerator Create(GeneratorAlgorithms algorithm)
        {
            switch (algorithm)
            {
                case GeneratorAlgorithms.DivideAndConquer:
                default:
                    return new DivideAndConquerAlgorithm();
            }
        }
    }
}
