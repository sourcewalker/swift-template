using System;

namespace Swift.Umbraco.$safeprojectname$.Utility
{
    public static class DateHelper
    {
        public static DateTimeOffset AddOffset(this DateTime dateTime, TimeZoneInfo timezone)
        {
            return new DateTimeOffset(dateTime, timezone.GetUtcOffset(dateTime));
        }

        public static DateTime ToUtc(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.UtcDateTime;
        }
    }
}
