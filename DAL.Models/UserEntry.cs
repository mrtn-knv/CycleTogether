using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using WebModels;

namespace DAL.Models
{
    public class UserEntry : EntityBase
    {           
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RouteEntry> Routes { get; set; }
        public int Level { get; set; }
        public List<Guid> Equipments { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }

        public UserEntry()
        {
            Routes = new List<RouteEntry>();
            Equipments = new List<Guid>();
        }
    }
}

