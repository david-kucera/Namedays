namespace Uniza.Namedays
{
    /// <summary>
    /// Structure represents day and month
    /// </summary>
    public record struct DayMonth
    {
        /// <summary>
        /// Returns day of daymonth
        /// </summary>
        public int Day { get; init; } 

        /// <summary>
        /// Returns month of daymonth
        /// </summary>
        public int Month { get; init; }

        /// <summary>
        /// Non parametrized constructor sets the values to the actual date.
        /// </summary>
        public DayMonth()
        {
            Day = DateTime.Now.Day;
            Month = DateTime.Now.Month;
        }

        /// <summary>
        /// Parametrized constructor sets the values to the values from parametres.
        /// </summary>
        /// <param name="day">Represents number of day.</param>
        /// <param name="month">Represents number of month.</param>
        public DayMonth(int day, int month)
        {
            Day = day;
            Month = month;
        }

        /// <summary>
        /// Converts values to DateTime.
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return new DateTime(DateTime.Now.Year, Month, Day);
        }
    }
}