using CycleTogether.Enums;
using System.Collections.Generic;

namespace DAL.Models
{
    public class UserEntry : EntityBase
    {           
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }

        public virtual IList<UserEquipmentEntry> UserEquipments { get; set; }
        public virtual IList<UserRouteEntry> UserRoutes { get; set; }
        public virtual ICollection<RouteEntry> Routes { get; set; }

    }
}

