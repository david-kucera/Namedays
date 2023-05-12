using System.Collections;
using System.Text.RegularExpressions;

namespace Uniza.Namedays
{
    public record NamedayCalendar : IEnumerable<Nameday>
    {
        private int NameCount { get; }
        private int DayCount { get; }

        private List<Nameday> _calendar = new List<Nameday>();

        private DayMonth? this[string name]
        {
            get
            {
                if (Contains(name))
                {
                    return GetEnumerator().Current.DayMonth;
                }
                return null;
            }
        }

        private string[] this[DayMonth dayMonth] => 
            (from meno in _calendar 
                where meno.DayMonth.Day == dayMonth.Day 
                    && meno.DayMonth.Month == dayMonth.Month 
                        select meno.Name).ToArray();

        private string[] this[DateOnly date] =>
            (from meno in _calendar
                where meno.DayMonth.Day == date.Day
                      && meno.DayMonth.Month == date.Month
                            select meno.Name).ToArray();

        private string[] this[DateTime date] =>
        (from meno in _calendar 
            where meno.DayMonth.Day == date.Day
              && meno.DayMonth.Month == date.Month
                select meno.Name).ToArray();

        private string[] this[int day, int month] =>
            (from meno in _calendar 
                where meno.DayMonth.Day == day
                      && meno.DayMonth.Month == month
                        select meno.Name).ToArray();
    
        /// <summary>
        /// Method returns enumerator.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Nameday> GetEnumerator()
        {
            return _calendar.GetEnumerator();
        }

        /// <summary>
        /// Method returns all namedays in calendar.
        /// </summary>
        /// <returns>All Namedays in calendar</returns>
        public IEnumerator<Nameday> GetNamedays()
        {
            return _calendar.GetEnumerator();
        }

        /// <summary>
        /// Method returns all namedays in that month.
        /// </summary>
        /// <param name="month"></param>
        /// <returns>All namedays in that month</returns>
        public IEnumerable<Nameday> GetNamedays(int month)
        {
            return from nameday in _calendar where nameday.DayMonth.Month == month select nameday;
        }

        /// <summary>
        /// Method returns all namedays mathing the input pattern.
        /// </summary>
        /// <param name="pattern">Pattern of which will be implemented</param>
        /// <returns></returns>
        public IEnumerable<Nameday> GetNamedays(string pattern)
        {
            return from nameday in _calendar where Regex.IsMatch(nameday.Name, pattern) select nameday;
        }

        /// <summary>
        /// Method adds nameday to calendar.
        /// </summary>
        /// <param name="nameday">Nameday of celebration</param>
        public void Add(Nameday nameday)
        {
            _calendar.Add(nameday);
        }

        /// <summary>
        /// Method adds one, or more names with given day and month to calendar.
        /// </summary>
        /// <param name="day">Day of celebration</param>
        /// <param name="month">Month of celebration</param>
        /// <param name="names">Names of celebrators</param>
        public void Add(int day, int month, params string[] names)
        {
            foreach (var name in names)
            {
                var dayMonth = new DayMonth(day, month);
                var novy = new Nameday(name, dayMonth);
                _calendar.Add(novy);
            } 
        }

        /// <summary>
        /// Method adds one or more names with given day and month to calendar.
        /// </summary>
        /// <param name="dayMonth">Day of celebration</param>
        /// <param name="names">Names of celebrators</param>
        public void Add(DayMonth dayMonth, params string[] names)
        {
            foreach (var name in names)
            {
                var nameday = new Nameday(name, dayMonth);
                _calendar.Add(nameday);
            }
        }

        /// <summary>
        /// Method removes given name from calendar, if found - returns true. If not found, returns false.
        /// </summary>
        /// <param name="name">Name to be removed</param>
        /// <returns>True, if successfully removed</returns>
        public bool Remove(string name)
        {
            if (Contains(name))
            {
                _calendar.Remove(GetEnumerator().Current);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method returns true, if given name is already in the calendar. If it does not, returns false.
        /// </summary>
        /// <param name="name">Name to be found</param>
        /// <returns>True, if found.</returns>
        public bool Contains(string name)
        {
            GetEnumerator().Reset();
            while (GetEnumerator().MoveNext())
            {
                if (GetEnumerator().Current.Name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method clears all data from calendar.
        /// </summary>
        public void Clear()
        {
            _calendar.Clear();
        }

        /// <summary>
        /// Method loads data of namedays from a csv file.
        /// </summary>
        /// <param name="csvFile">csv file</param>
        public void Load(FileInfo csvFile)
        {
            using var reader = new StreamReader(csvFile.FullName);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var date = values[0];
                var date_splitted = date.Split(" ");

                var day_with_comma = date_splitted[0];
                var day = day_with_comma.Substring(0,day_with_comma.Length-1);
                var month_with_comma = date_splitted[1];
                var month = month_with_comma.Substring(0, month_with_comma.Length - 1);

                var dayMonth = new DayMonth(int.Parse(day), int.Parse(month));

                for (var i = 1; i < values.Length; i++)
                {
                    if (values[i] != "-" || values[i] != "")
                    {
                        var nameday = new Nameday(values[i], dayMonth);
                        _calendar.Add(nameday);
                    }
                }
            }
        }

        /// <summary>
        /// Method saves data from calendar to csv file.
        /// </summary>
        /// <param name="csvFile"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Write(FileInfo csvFile)
        {
            // TODO implement
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
