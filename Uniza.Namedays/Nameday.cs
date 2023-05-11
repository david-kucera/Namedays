using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniza.Namedays
{
    /// <summary>
    /// Structure represents nameday celebration day.
    /// </summary>
    public record struct Nameday
    {
        public string Name { get; init; }
        public DayMonth DayMonth { get; init; }

        /// <summary>
        /// Non parametrized constructor.
        /// </summary>
        public Nameday() { }

        /// <summary>
        /// Parametrized constructor, sets values according to the parametres.
        /// </summary>
        /// <param name="name">Name of celebrator.</param>
        /// <param name="dayMonth">DayMonth of celebration.</param>
        public Nameday(string name, DayMonth dayMonth)
        {
            Name = name;
            DayMonth = dayMonth;
        }
    }
}
