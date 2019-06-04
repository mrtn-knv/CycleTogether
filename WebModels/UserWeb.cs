using CycleTogether.Enums;
using System;
using System.Collections.Generic;

namespace WebModels
{
    public class UserWeb
    {       
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RouteWeb> Routes { get; set; }
        public List<Guid> Equipments { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
