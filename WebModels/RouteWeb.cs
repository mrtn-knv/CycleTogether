using System;
using System.Collections.Generic;

namespace WebModels
{
    public class RouteWeb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public List<UserWeb> Members { get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan? Duration { get; set; }

        public enum Type
        {
            Classic,
            MountainBike,
            Family,
            Epic
        }
        public enum Difficulty
        {
            Leisurely,
            Moderate,
            Tough,
            MultiLevel
        }
    }
}
