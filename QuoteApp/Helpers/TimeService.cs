using System;

namespace QuoteApp.Helpers
{
    public class TimeService
    {
        public DateTime GetCurrentQuarterStartDate()
        {
            return GetQuarterStartOrEnd(true);
        }

        public DateTime GetCurrentQuarterEndDate()
        {
            return GetQuarterStartOrEnd(false);
        }

        private DateTime GetQuarterStartOrEnd(bool start)
        {
            int end = start ? 0 : 2;
            int day;
            switch (DateTime.UtcNow.Month)
            {
                case 1:
                case 2:
                case 3:
                    day = start ? 1 : DateTime.DaysInMonth(DateTime.UtcNow.Year, 3);
                    return new DateTime(DateTime.UtcNow.Year, 1 + end, day);
                case 4:
                case 5:
                case 6:
                    day = start ? 1 : DateTime.DaysInMonth(DateTime.UtcNow.Year, 6);
                    return new DateTime(DateTime.UtcNow.Year, 4 + end, day);
                case 7:
                case 8:
                case 9:
                    day = start ? 1 : DateTime.DaysInMonth(DateTime.UtcNow.Year, 9);
                    return new DateTime(DateTime.UtcNow.Year, 7 + end, day);
                case 10:
                case 11:
                case 12:
                    day = start ? 1 : DateTime.DaysInMonth(DateTime.UtcNow.Year, 12);
                    return new DateTime(DateTime.UtcNow.Year, 10 + end, day);
                default: throw new Exception("Not possible");
            }
        }
    }
}