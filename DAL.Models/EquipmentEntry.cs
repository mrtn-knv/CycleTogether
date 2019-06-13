using System.Collections.Generic;


namespace DAL.Models
{
    public class EquipmentEntry : EntityBase
    {
        public string Name { get; set; }

        public IList<UserEquipment> UserEquipments { get; set; }
        public IList<RouteEquipment> RouteEquipments { get; set; }
        public EquipmentEntry()
        {

        }
    }
}
