using CycleTogether.Enums;
using System;
using System.Collections.Generic;

namespace WebModels
{
    public class Route
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string Info { get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }
        public bool SuitableForKids { get; set; }
        public bool IsComplete { get; set; }
        public IEnumerable<UserRoute> Subscribed { get; set; }
        public IEnumerable<RouteEquipments> Equipments{ get; set; }
        public IEnumerable<Picture> Images { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }
        
    }
}
