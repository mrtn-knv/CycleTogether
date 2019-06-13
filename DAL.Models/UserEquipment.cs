using System;

namespace DAL.Models
{
    public class UserEquipment
    {
        public Guid UserId { get; set; }
        public UserEntry User { get; set; }

        public Guid EquipmentId { get; set; }
        public EquipmentEntry Equipment { get; set; }
    }
}
