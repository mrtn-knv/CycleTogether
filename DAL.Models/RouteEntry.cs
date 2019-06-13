using CycleTogether.Enums;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class RouteEntry : EntityBase
    {
        public string Name { get; set; }        
        public string Info { get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }
        public bool SuitableForKids { get; set; }
        public bool IsComplete { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }

        public Guid UserId { get; set; }
        public UserEntry User { get; set; }
        public IList<UserRoute> UserRoutes { get; set; }
        public IList<RouteEquipment> RouteEquipments { get; set; }
        public ICollection<PictureEntry> Pictures { get; set; }

        public RouteEntry()
        {

        }

    }
}
