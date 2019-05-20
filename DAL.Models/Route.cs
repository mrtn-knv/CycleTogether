using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Route : EntityBase
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public List<User> Subscribed { get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }

        public enum Type
        {
            OneDay,
            Multiday
        }
        public enum Difficulty
        {
            Beginner,
            Intermediate,
            Expert
        }

        public Route()
        {

        }

    }
}
