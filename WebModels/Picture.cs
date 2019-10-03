using System;
using System.ComponentModel.DataAnnotations;

namespace WebModels
{
    public class Picture
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        [Required]
        public Guid RouteId { get; set; }
        public string Path { get; set; }

    }
}
