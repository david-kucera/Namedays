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
        private string Name { get; init; }
        private DayMonth DayMonth { get; init; }

        /// <summary>
        /// Non parametrized constructor, sets Name to empty string and DayMonth to actual day.
        /// </summary>
        public Nameday()
        {
            Name = string.Empty;
            DayMonth = new DayMonth();
        }

        /// <summary>
        /// Parametrized constructor, sets values according to the parametres.
        /// </summary>
        /// <param name="name">name of celebrator.</param>
        /// <param name="dayMonth">DayMonth of celebration.</param>
        public Nameday(string name, DayMonth dayMonth)
        {
            Name = name;
            DayMonth = dayMonth;
        }
    }
}
