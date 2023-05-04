namespace Uniza.Namedays
{
    public record struct DayMonth
    {
        private int Day { get; init; }
        public int Month { get; init; }

        public DayMonth()
        {
            Day = DateTime.Now.Day;
            Month = DateTime.Now.Month;
        }

        public DayMonth(int day, int month)
        {
            Day = day;
            Month = month;
        }

        public DateTime ToDateTime()
        {
            return new DateTime(DateTime.Now.Year, Month, Day);
        }
    }
}