
using System.Collections.Generic;

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

        public User()
        {
            Routes = new List<Route>();
        }
    }
}

