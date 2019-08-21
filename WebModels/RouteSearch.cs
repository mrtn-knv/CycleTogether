using System;
using System.Collections.Generic;
using System.Text;

namespace WebModels
{
    public class RouteSearch
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string StartPoint { get; set; }
        public DateTime StartTime { get; set; }
    }
}
