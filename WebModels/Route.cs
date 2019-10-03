using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebModels
{
    public class Route
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        public Guid UserId { get; set; }
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
        public bool IsComplete => DateTime.Compare(DateTime.UtcNow, StartTime) > 0;
        public IEnumerable<UserRoute> Subscribed { get; set; }
        public IEnumerable<RouteEquipments> Equipments{ get; set; }
        public IEnumerable<Guid> EquipmentsIds { get; set; }
        public IEnumerable<Picture> Images { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }

    }
}
