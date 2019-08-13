using System;

namespace DAL.Models
{
    public class RouteEquipmentEntry : EntityBase
    {
        public Guid RouteId { get; set; }
        public virtual RouteEntry Route { get; set; }

        public Guid EquipmentId { get; set; }
        public virtual EquipmentEntry Equipment { get; set; }
    }
}
