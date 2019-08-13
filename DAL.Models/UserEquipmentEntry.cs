using System;

namespace DAL.Models
{
    public class UserEquipmentEntry : EntityBase
    {
        public Guid UserId { get; set; }
        public virtual UserEntry User { get; set; }

        public Guid EquipmentId { get; set; }
        public virtual EquipmentEntry Equipment { get; set; }
    }
}
