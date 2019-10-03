using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class RouteEntry : EntityBase
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        public string Info { get; set; }
        [Required]
        public string StartPoint { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public bool SuitableForKids { get; set; }
        public bool IsComplete { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public virtual UserEntry User { get; set; }
        public virtual IList<UserRouteEntry> UserRoutes { get; set; }
        public virtual IList<RouteEquipmentEntry> RouteEquipments { get; set; }
        public virtual ICollection<PictureEntry> Pictures { get; set; }

    }
}
