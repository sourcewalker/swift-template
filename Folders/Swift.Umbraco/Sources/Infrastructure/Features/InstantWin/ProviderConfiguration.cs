using Models.DTO;
using System;

namespace $safeprojectname$.$safeprojectname$.InstantWin
{
    public struct ProviderConfiguration
    {
        public struct Campaign
        {
            public static DateTimeOffset StartDate = new DateTime(2019, 07, 29, 0, 0, 0);

            public static DateTimeOffset EndDate = new DateTime(2019, 09, 01, 23, 59, 59);

            // DateTime just for storing the time, date information is not relevant
            public static DateTimeOffset OpenTime = new DateTime(2019, 07, 29, 8, 0, 0);

            // DateTime just for storing the time, date information is not relevant
            public static DateTimeOffset CloseTime = new DateTime(2019, 07, 29, 21, 0, 0);
        }

        public struct Generator
        {
            public static int LimitNumber = 3;

            public static GeneratorLimitOptions limitOption = GeneratorLimitOptions.LimitPerHour;

            public static GeneratorAlgorithms algorithm = GeneratorAlgorithms.DivideAndConquer;
        }

        public struct Allocator
        {
            public static AllocatorAlgorithms algorithm = AllocatorAlgorithms.Weighted;
        }
    }
}
