using CycleTogether.Enums;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Route : EntityBase
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public Guid CreatedBy { get; set; }
        public List<string> SubscribedMails { get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsComplete { get; set; }
        public bool SuitableForKids { get; set; }
        public Equipment EquipmentsNeeded { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }

        public Route()
        {
            SubscribedMails = new List<string>();
        }

    }
}
