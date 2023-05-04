using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniza.Namedays
{
    public record struct Nameday
    {
        private string Name { get; init; }
        private DayMonth DayMonth { get; init; }

        public Nameday()
        {
            Name = "";
            DayMonth = new DayMonth();
        }

        public Nameday(string name, DayMonth dayMonth)
        {
            Name = name;
            DayMonth = dayMonth;
        }
    }
}
