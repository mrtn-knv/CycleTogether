using CycleTogether.Enums;
using System.Collections.Generic;
using WebModels;

namespace DAL.Models
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Route> Routes { get; set; }
        public int Level { get; set; }
        public Equipment Equipments { get; set; }
        public Terrain Terrain { get; set; }
        public Endurance Endurance { get; set; }
        public TypeOfRoute Type { get; set; }
        public Difficulty Difficulty { get; set; }

        public User()
        {
            Routes = new List<Route>();
        }
    }
}

