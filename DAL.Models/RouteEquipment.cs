using System;

namespace DAL.Models
{
    public class RouteEquipment
    {
        public Guid RouteId { get; set; }
        public RouteEntry Route { get; set; }

        public Guid EquipmentId { get; set; }
        public EquipmentEntry Equipment { get; set; }
    }
}
