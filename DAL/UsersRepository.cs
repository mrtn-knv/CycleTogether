using DAL.Contracts;
using DAL.Models;
using System.Linq;

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
