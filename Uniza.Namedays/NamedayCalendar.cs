using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Uniza.Namedays
{
    public record NamedayCalendar : IEnumerable<Nameday>
    {
        /// <summary>
        /// Returns overall number of names in the calendar.
        /// </summary>
        public int NameCount => _name_count;

        /// <summary>
        /// Returns overall number of dates filled with names in the calendar.
        /// </summary>
        public int DayCount => _day_count;

        private List<Nameday>.Enumerator _enumerator;
        private List<Nameday> _calendar = new List<Nameday>();
        private int _name_count;
        private int _day_count;

        /// <summary>
        /// Indexer which returns dayMonth of input name.
        /// </summary>
        /// <param name="name">Name of celebrator.</param>
        /// <returns>DayMonth of his celebration.</returns>
        public DayMonth? this[string name]
        {
            get
            {
                if (Contains(name))
                {
                    return _enumerator.Current.DayMonth;
                }
                return null;
            }
        }

        /// <summary>
        /// Indexer returns string array of names that celebrate on given daymonth.
        /// </summary>
        /// <param name="dayMonth">DayMonth of wanted given celebrators.</param>
        /// <returns>String array with that day celebrators.</returns>
        public string[] this[DayMonth dayMonth] => 
            (from meno in _calendar 
                where meno.DayMonth.Day == dayMonth.Day 
                    && meno.DayMonth.Month == dayMonth.Month 
                        select meno.Name).ToArray();

        /// <summary>
        /// Indexer returns string array of names that celebrate on given DateOnly.
        /// </summary>
        /// <param name="date">DateOnly with wanted date of celebration.</param>
        /// <returns>String array of that day celebrators.</returns>
        public string[] this[DateOnly date] =>
            (from meno in _calendar
                where meno.DayMonth.Day == date.Day
                      && meno.DayMonth.Month == date.Month
                            select meno.Name).ToArray();

        /// <summary>
        /// Indexer returns string array of names that celebrate on given DateTime.
        /// </summary>
        /// <param name="date">DateTime with wanted date of celebration.</param>
        /// <returns>String array of that day celebrators.</returns>
        public string[] this[DateTime date] =>
        (from meno in _calendar 
            where meno.DayMonth.Day == date.Day
              && meno.DayMonth.Month == date.Month
                select meno.Name).ToArray();

        /// <summary>
        /// Indexer returns string array of names that celebrate on given day and month.
        /// </summary>
        /// <param name="day">Integer day of celebration.</param>
        /// <param name="month">Integer month of celebration.</param>
        /// <returns>String array of that day celebrators.</returns>
        public string[] this[int day, int month] =>
            (from meno in _calendar 
                where meno.DayMonth.Day == day
                      && meno.DayMonth.Month == month
                        select meno.Name).ToArray();
    
        /// <summary>
        /// Method returns all celebrations in the calendar.
        /// </summary>
        /// <returns>All namedays that are in the calendar.</returns>
        public IEnumerator<Nameday> GetEnumerator()
        {
            return _calendar.GetEnumerator();
        }

        /// <summary>
        /// Method returns all namedays in calendar.
        /// </summary>
        /// <returns>All Namedays in calendar.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns all namedays in calendar.
        /// </summary>
        /// <returns>Returns all namedays in the calendar.</returns>
        public IEnumerable<Nameday> GetNamedays()
        {
            return _calendar;
        }

        /// <summary>
        /// Method returns namedays which name length equals the parameter numberOfChars.
        /// </summary>
        /// <param name="numberOfChars">Number of characters.</param>
        /// <param name="boo">Just another param, to diffrenciate from other method.</param>
        /// <returns></returns>
        public IEnumerable<Nameday> GetNamedays(int numberOfChars, bool boo)
        {
            return from nameday in _calendar where nameday.Name.Length == numberOfChars select nameday;
        }

        /// <summary>
        /// Method returns all namedays in that month.
        /// </summary>
        /// <param name="month">Integer of month of celebration.</param>
        /// <returns>All namedays in that month.</returns>
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
        /// <param name="nameday">Nameday of celebration.</param>
        public void Add(Nameday nameday)
        {
            
            _calendar.Add(nameday);
            _name_count++;
            // If this day has not been filled with celebration yet, increase _day_count too.
            if (this[nameday.DayMonth].Length == 0)
            {
                _day_count++;
            }
        }

        /// <summary>
        /// Method adds one, or more names with given day and month to calendar.
        /// </summary>
        /// <param name="day">Day of celebration.</param>
        /// <param name="month">Month of celebration.</param>
        /// <param name="names">Names of celebrators.</param>
        public void Add(int day, int month, params string[] names)
        {
            if (this[day, month].Length == 0)
            {
                _day_count++;
            }
            foreach (var name in names)
            {
                var dayMonth = new DayMonth(day, month);
                var newNameday = new Nameday(name, dayMonth);
                _calendar.Add(newNameday);
                _name_count++;
            } 
        }

        /// <summary>
        /// Method adds one or more names with given day and month to calendar.
        /// </summary>
        /// <param name="dayMonth">Day of celebration.</param>
        /// <param name="names">Names of celebrators.</param>
        public void Add(DayMonth dayMonth, params string[] names)
        {
            if (this[dayMonth].Length == 0)
            {
                _day_count++;
            }
            foreach (var name in names)
            {
                var nameday = new Nameday(name, dayMonth);
                _calendar.Add(nameday);
                _name_count++;
            }
        }

        /// <summary>
        /// Method removes given name from calendar, if found - returns true. If not found, returns false.
        /// </summary>
        /// <param name="name">Name to be removed.</param>
        /// <returns>True, if successfully removed.</returns>
        public bool Remove(string name)
        {
            if (Contains(name) && _calendar.Remove(_enumerator.Current))
            {
                _name_count--;
                // If that day does not contain any other name, decrement _day_count
                var dayMonth = this[name];
                if (dayMonth.Equals(null))
                {
                    _day_count--;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method returns true, if given name is already in the calendar. If it does not, returns false.
        /// </summary>
        /// <param name="name">Name to be found in the calendar.</param>
        /// <returns>True, if found.</returns>
        public bool Contains(string name)
        {
            _enumerator = (List<Nameday>.Enumerator)GetEnumerator();
            while (_enumerator.MoveNext())
            {
                if (_enumerator.Current.Name.Equals(name))
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
                var values = line.Split(';');

                var date = values[0];
                var date_splitted = date.Split(" ");

                var day_with_comma = date_splitted[0];
                var day = day_with_comma.Substring(0,day_with_comma.Length - 1);
                var month_with_comma = date_splitted[1];
                var month = month_with_comma.Substring(0, month_with_comma.Length - 1);

                var dayMonth = new DayMonth(int.Parse(day), int.Parse(month));

                for (var i = 1; i < values.Length; i++)
                {
                    // check if name is not empty
                    if (values[i].Contains('-') || values[i].Equals(""))
                    {
                        continue;
                    }
                    var nameday = new Nameday(values[i], dayMonth);
                    _calendar.Add(nameday);
                }
            }
            reader.Close();
        }

        /// <summary>
        /// Method saves data from calendar to csv file.
        /// </summary>
        /// <param name="csvFile">CSV file</param>
        public void Write(FileInfo csvFile)
        {
            // TODO implement this
            using var writer = new StreamWriter(csvFile.FullName);
            foreach (var line in _calendar.Select(nameday => new string[] { nameday.DayMonth.ToString(), nameday.Name }).Select(data => string.Join(";", data)))
            {
                writer.WriteLine(line);
            }
            writer.Close();
        }
    }
}
