using System;

namespace DAL.Models
{
    public class UserRoute
    {
        public Guid UserId { get; set; }
        public UserEntry User { get; set; }

        public Guid RouteId { get; set; }
        public RouteEntry Route { get; set; }
    }
}
