namespace Uniza.Namedays
{
    /// <summary>
    /// Structure represents nameday celebration day.
    /// </summary>
    public record struct Nameday
    {
        /// <summary>
        /// Returns name of the celebrator.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Returns daymoth of celebration.
        /// </summary>
        public DayMonth DayMonth { get; init; }

        /// <summary>
        /// Non parametrized constructor.
        /// </summary>
        public Nameday()
        {
            Name = "";
        }

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
