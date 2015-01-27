using System;

namespace QuoteApp.Database.Time
{
    public class Quarter
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public Quarter()
        {
            SetQuarterBoundaries(DateTime.UtcNow);
        }

        public Quarter(DateTime dateInQuarterToSet)
        {
            SetQuarterBoundaries(dateInQuarterToSet);
        }

        private void SetQuarterBoundaries(DateTime dateInQuarterToSet)
        {
            int quarterNumber = (dateInQuarterToSet.Month - 1) / 3 + 1;
            Start = new DateTime(dateInQuarterToSet.Year, (quarterNumber - 1) * 3 + 1, 1);
            End = Start.AddMonths(3).AddDays(-1);
        }
    }
}