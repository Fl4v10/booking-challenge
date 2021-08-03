using System;

namespace BookChallenge.Utils
{
    public static class DateTimeExtensions
    {
        public static bool IsGreaterThanOrEqual(this DateTime start, DateTime end)
        {
            var result = DateTime.Compare(start.Date, end.Date);
            return (result == 0 || result > 0);
        }
    }
}
