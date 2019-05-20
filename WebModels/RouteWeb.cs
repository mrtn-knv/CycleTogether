using System;
using System.Collections.Generic;

namespace WebModels
{
    public class RouteWeb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserWeb CreatedBy { get; set; }
        public string Info { get; set; }
        public List<string> SubscribedMails = new List<string>();
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }

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
    }
}
