using System;


namespace WebModels
{
    public class Picture
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public Guid RouteId { get; set; }
        public string Path { get; set; }

    }
}
