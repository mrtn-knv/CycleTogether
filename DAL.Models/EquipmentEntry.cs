using System.Collections.Generic;


namespace DAL.Models
{
    public class EquipmentEntry : EntityBase
    {
        public string Name { get; set; }

        public virtual IList<UserEquipment> UserEquipments { get; set; }
        public virtual IList<RouteEquipment> RouteEquipments { get; set; }

    }
}
