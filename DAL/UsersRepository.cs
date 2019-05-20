using DAL.Contracts;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class UsersRepository : Repository<User>, IUserRepository
    {        
        public UsersRepository() 
        {
            context.Add(new User {Email = "mrtn.knv@abv.bg", Password = "$2y$12$Es/hsGSHBCBRVtzF4aVCbO.nzzB64B3kjgoMYEpnAy623Yndi8.CC", FirstName = "mrtn", LastName = "knv" });
        }
        public User GetByEmail(string email)
        {
            return context.FirstOrDefault(u => u.Email == email);            
        }

    }
}
