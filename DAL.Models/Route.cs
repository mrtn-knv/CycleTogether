using CycleTogether.Enums;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Route : EntityBase
    {
        public string Name { get; set; }
        public Guid CreatedBy { get; set; }
        public string Info { get; set; }
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }
        public bool SuitableForKids { get; set; }
        public bool IsComplete { get; set; }
        public List<string> SubscribedMails = new List<string>();
        public List<Guid> Equipments { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }

        public Route()
        {
            Equipments = new List<Guid>();
            SubscribedMails = new List<string>();
        }

    }
}
