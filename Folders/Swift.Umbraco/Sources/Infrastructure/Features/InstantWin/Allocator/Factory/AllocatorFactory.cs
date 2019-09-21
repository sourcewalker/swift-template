using $safeprojectname$.$safeprojectname$.InstantWin.Allocator.Algorithms;
using $safeprojectname$.$safeprojectname$.InstantWin.Interfaces;

namespace $safeprojectname$.$safeprojectname$.InstantWin.Allocator.Factory
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
