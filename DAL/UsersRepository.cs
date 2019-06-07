using DAL.Contracts;
using DAL.Models;
using System;
using System.Linq;

namespace DAL
{
    public class UsersRepository : Repository<UserEntry>, IUserRepository
    {        
        public UsersRepository() 
        {
            context.Add(new UserEntry { Id = Guid.NewGuid(), Email = "mrtn.knv@abv.bg", Password = "$2y$12$Es/hsGSHBCBRVtzF4aVCbO.nzzB64B3kjgoMYEpnAy623Yndi8.CC", FirstName = "mrtn", LastName = "knv" });
        }

        public void AddRoute(RouteEntry route, Guid id)
        {
            context.FirstOrDefault(u => u.Id == id).Routes.Add(route);
        }

        public UserEntry GetByEmail(string email)
        {
            return context.FirstOrDefault(u => u.Email == email);            
        }

        

    }
}
