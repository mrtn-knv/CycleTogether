using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Route : EntityBase
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public User CreatedBy { get; set; }
        public List<string> SubscribedMails { get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }
        public bool Iscomplete { get; set; }

        public enum Type
        {
            OneDay,
            SeveralDays
        }

        public enum Difficulty
        {
            Beginner,
            Intermediate,
            Expert
        }

        public Route()
        {
            SubscribedMails = new List<string>();
        }

    }
}
