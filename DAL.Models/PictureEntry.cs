using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class PictureEntry : EntityBase
    {
        [Required]
        public string PublicId { get; set; }
        public string Path { get; set; }

        [Required]
        public Guid RouteId { get; set; }
        public virtual RouteEntry Route { get; set; }

    }
}
