
using System;
using System.Collections;
using System.Globalization;
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
            && meno.DayMonth.Month == dayMonth.Month select meno.Name).ToArray();

        private string[] this[DateOnly date]
        {
            // TODO implement
            get { throw new NotImplementedException(); }
        }

        private string[] this[DateTime date]
        {
            // TODO implement
            get { throw new NotImplementedException(); }
        }

        private string[] this[int day, int month]
        {
            // TODO implement
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<Nameday> GetEnumerator()
        {
            return _calendar.GetEnumerator();
        }

        /// <summary>
        /// metoda vrati vsetky meniny v kalendari
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Nameday> GetNamedays()
        {
            return _calendar.GetEnumerator();
        }

        /// <summary>
        /// metoda vrati vsetky meniny v danom mesiaci
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Nameday> GetNamedays(int month)
        {
            return from nameday in _calendar where nameday.DayMonth.Month == month select nameday;
        }

        /// <summary>
        /// metóda vráti všetky meniny, ktoré zodpovedajú zadanému reťazcu regulárneho výrazu (pattern), ktorý sa bude aplikovať na mená v kalendári.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public IEnumerable<Nameday> GetNamedays(string pattern)
        {
            return from nameday in _calendar where Regex.IsMatch(nameday.Name, pattern) select nameday;
        }

        /// <summary>
        /// metóda pridá meniny do kalendára.
        /// </summary>
        /// <param name="nameday"></param>
        public void Add(Nameday nameday)
        {
            _calendar.Add(nameday);
        }

        /// <summary>
        /// metóda pridá jedno alebo viacero mien so zadaným dňom a mesiacom oslavy do kalendára.
        /// </summary>
        /// <param name="day">Den oslavy</param>
        /// <param name="month">Mesiac oslavy</param>
        /// <param name="names">Mena oslavencov</param>
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
        /// metóda pridá jedno alebo viacero mien so zadaným dňom a mesiacom oslavy do kalendára.
        /// </summary>
        /// <param name="dayMonth">Den oslavy ako dayMonth strukctura</param>
        /// <param name="names">Mena oslavencov</param>
        public void Add(DayMonth dayMonth, params string[] names)
        {
            foreach (var name in names)
            {
                var nameday = new Nameday(name, dayMonth);
                _calendar.Add(nameday);
            }
        }

        /// <summary>
        /// metóda odstráni meno z kalendára mien. Ak ho nájde a odstráni, vráti hodnotu true. Ak ho nenájde, nevyhodí žiadnu výnimku, ale vráti hodnotu false.
        /// </summary>
        /// <param name="name">Meno na vyhodenie</param>
        /// <returns></returns>
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
        /// metóda vráti true, ak zadané meno v kalendári existuje. Ak neexistuje, vráti hodnotu false.
        /// </summary>
        /// <param name="name">Meno hladaneho</param>
        /// <returns></returns>
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
        /// metóda vymaže všetky údaje z kalendára.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Clear()
        {
            _calendar.Clear();
        }

        public void Load(FileInfo csvFile)
        {
            // TODO implement
            // metóda načíta kalendár mien zo súboru s príponou CSV.
            throw new NotImplementedException();
        }

        public void Write(FileInfo csvFile)
        {
            // TODO implement
            // metóda zapíše kalendár mien do súboru s príponou CSV.
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
