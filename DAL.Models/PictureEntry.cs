using System;

namespace DAL.Models
{
    public class PictureEntry : EntityBase
    {
        public string PublicId { get; set; }
        public string Path { get; set; }

        public Guid RouteId { get; set; }
        public virtual RouteEntry Route { get; set; }

    }
}
