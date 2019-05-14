using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels;

namespace DAL
{
    public class UsersRepository : Repository<User>, IUserRepository
    {      
        public UsersRepository() 
        {
            
          
        }

        public User GetByEmail(string email)
        {
            return context.FirstOrDefault(u => u.Email == email);            
        }
     
    }
}
