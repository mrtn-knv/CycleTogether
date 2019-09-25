using System;
using System.Collections.Generic;
using System.Text;

namespace WebModels
{
    public class RouteView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
