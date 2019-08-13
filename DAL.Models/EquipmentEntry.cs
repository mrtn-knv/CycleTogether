using System.Collections.Generic;


namespace DAL.Models
{
    public class EquipmentEntry : EntityBase
    {
        public string Name { get; set; }

        public virtual IList<UserEquipmentEntry> UserEquipments { get; set; }
        public virtual IList<RouteEquipmentEntry> RouteEquipments { get; set; }

    }
}
