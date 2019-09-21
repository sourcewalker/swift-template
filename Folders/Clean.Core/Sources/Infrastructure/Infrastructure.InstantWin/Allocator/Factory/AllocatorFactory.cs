using $safeprojectname$.Allocator.Algorithms;
using $safeprojectname$.Enums;
using $safeprojectname$.Interfaces;

namespace $safeprojectname$.Allocator.Factory
{
    public static class AllocatorFactory
    {
        public static IAllocator Create(AllocatorAlgorithms algorithm)
        {
            switch (algorithm)
            {
                case AllocatorAlgorithms.Blind:
                    return new BlindAlgorithm();
                case AllocatorAlgorithms.Weighted:
                    return new WeightedAlgorithm();
                case AllocatorAlgorithms.Fair:
                default:
                    return new FairAlgorithm();
            }
        }
    }
}
