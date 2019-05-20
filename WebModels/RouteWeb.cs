using System;
using System.Collections.Generic;
using System.Text;

namespace WebModels
{
    public class RouteWeb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int MembersRequired { get; set; }
        public List<UserWeb> Members { get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
