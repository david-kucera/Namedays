
using System.Collections;

namespace Uniza.Namedays
{
    public record NamedayCalendar : IEnumerable<Nameday>
    {
        private int NameCount { get; }
        private int DayCount { get; }

        private DayMonth? this[string name]
        {
            // TODO implement
            get { throw new NotImplementedException(); }
        }

        private string[] this[DayMonth dayMonth]
        {
            // TODO implement
            get { throw new NotImplementedException(); }
        }

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
            // TODO implement
            throw new NotImplementedException();
        }

        public IEnumerator<Nameday> GetNamedays()
        {
            // TODO implement
            // metoda vrati vsetky meniny v kalendari
            throw new NotImplementedException();
        }

        public IEnumerator<Nameday> GetNamedays(int month)
        {
            // TODO implement
            // metoda vrati vsetky meniny v danom mesiaci
            throw new NotImplementedException();
        }

        public IEnumerator<Nameday> GetNamedays(string pattern)
        {
            // TODO implement
            // metóda vráti všetky meniny, ktoré zodpovedajú zadanému reťazcu regulárneho výrazu (pattern), ktorý sa bude aplikovať na mená v kalendári.
            throw new NotImplementedException();
        }

        public void Add(Nameday nameday)
        {
            // TODO implement
            // metóda pridá meniny do kalendára.
        }

        public void Add(int day, int month, params string[] names)
        {
            // TODO implement
            // metóda pridá jedno alebo viacero mien so zadaným dňom a mesiacom oslavy do kalendára.
        }

        public void Add(DayMonth dayMonth, params string[] names)
        {
            // TODO implement
            // metóda pridá jedno alebo viacero mien so zadaným dňom a mesiacom oslavy do kalendára.
        }

        public bool Remove(string name)
        {
            // TODO implement
            // metóda odstráni meno z kalendára mien. Ak ho nájde a odstráni, vráti hodnotu true. Ak ho nenájde, nevyhodí žiadnu výnimku, ale vráti hodnotu false.
            throw new NotImplementedException(); 
        }

        public bool Contains(string name)
        {
            // TODO implement
            // metóda vráti true, ak zadané meno v kalendári existuje. Ak neexistuje, vráti hodnotu false.
            throw new NotImplementedException();
        }

        public void Clear()
        {
            // TODO implement
            // metóda vymaže všetky údaje z kalendára.
            throw new NotImplementedException();
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
