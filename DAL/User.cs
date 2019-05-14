using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public  class User : EntityBase
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }        

        public User()
        {
           
        }
    }
}
