using System;

namespace $safeprojectname$.Extensions.Moment
{
    public static class TimeSpanHelper
    {
        public static DateTime ToTodayDateTime(this TimeSpan hour)
        {
            var todayBegin = DateTime.Today;
            return todayBegin.Add(hour);
        }
    }
}