using $safeprojectname$.$safeprojectname$.InstantWin.Generator.Algorithms;
using $safeprojectname$.$safeprojectname$.InstantWin.Interfaces;

namespace $safeprojectname$.$safeprojectname$.InstantWin.Generator.Factory
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
