﻿using System;

namespace DAL.Models
{
    public class UserRouteEntry : EntityBase
    {
        public Guid UserId { get; set; }
        public virtual UserEntry User { get; set; }

        public Guid RouteId { get; set; }
        public virtual RouteEntry Route { get; set; }
    }
}
