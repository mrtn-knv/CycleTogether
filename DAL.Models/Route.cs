using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Route : EntityBase
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public int MembersRequired { get; set; }
        public List<User> Members { get; set; }
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

        public Route()
        {

        }

    }
}
