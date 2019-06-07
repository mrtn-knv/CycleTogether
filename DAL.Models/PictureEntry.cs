using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class PictureEntry : EntityBase
    {
        public Guid RouteId { get; set; }
        public string PublicId { get; set; }
        public string Path { get; set; }

        public PictureEntry()
        {

        }
    }
}
